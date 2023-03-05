using Music_Player.Model;
using Music_Player.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Music_Player.ViewModel;

internal class CollectionsPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObservableCollection<Playlist> PlaylistsOfTheDay { get; set; }
    public ObservableCollection<Playlist> NewHot { get; set; }

    public ICommand UpdatePage => new Command(async ()=>
    {
        IsRefresh = true;
        await Task.Delay(1000);
        ReloadLists();
        IsRefresh = false;
    });

    // everyday playlist
    public Playlist Everyday { get; set; }
    public int EverydayLength { get => Everyday.SongsAmount; }
    public Color EverydayColor { get => PlaylistsController.Colors[Globals.Random.Next(0, PlaylistsController.Colors.Count)]; }

    public bool IsRefresh
    {
        get => _isRefresh;
        set
        {
            if (_isRefresh == value) return;

            _isRefresh = value;
            RaisePropertyChanged();
        }
    }
    private bool _isRefresh;

    private static List<int> _localCounts = new List<int>();

    public CollectionsPageViewModel() => InitializeLists();

    private void InitializeLists()
    {
        PlaylistsOfTheDay = new ObservableCollection<Playlist>();
        NewHot = new ObservableCollection<Playlist>();

        Load();
    }

    private void ReloadLists()
    {
        PlaylistsOfTheDay.Clear();
        NewHot.Clear();

        Load();
    }

    private void Load()
    {
        _localCounts.Clear();
        for (int i = 0; i < PlaylistsController.PlaylistNames.Count; i++) _localCounts.Add(0);

        CreateEverydayPlaylist();

        FillPlaylistsSection(PlaylistsOfTheDay, 30);
        FillPlaylistsSection(NewHot, 20);
    }

    public void ChoosePlaylist(string name)
    {
        if (name == null) return;

        Playlist newPlaylist = FindPlaylist(name);

        Player.UpdateQueue(newPlaylist.SongTitles.ToList(), name);
    }

    public void OpenPlaylistPage(string name)
    {
        if (name == null) return;

        Playlist clickedPlaylist = FindPlaylist(name);

        PlaylistInfoPageOpen();
        UpdatePlaylistInfoPage(clickedPlaylist);
    }

    private void UpdatePlaylistInfoPage(Playlist clickedOne) => ModelsController.UpdatePlaylistInfo(clickedOne);

    private async void PlaylistInfoPageOpen() => await Shell.Current.GoToAsync(nameof(PlaylistInfoPage));

    private Playlist FindPlaylist(string name)
    {
        for (int i = 0; i < PlaylistsOfTheDay.Count; i++) // check if necessary playlist in 'of the day' section
            if (PlaylistsOfTheDay[i].PlaylistName.Equals(name))
                return PlaylistsOfTheDay[i];

        for (int i = 0; i < NewHot.Count; i++) // check if necessary playlist in 'new & hot' section
            if (NewHot[i].PlaylistName.Equals(name))
                return NewHot[i];

        if (Everyday.PlaylistName.Equals(name)) return Everyday; // check if needed playlist is 'everyday'

        return new Playlist("Locker");
    }

    private void CreateEverydayPlaylist()
    {
        Everyday = new Playlist("Everyday Chill");

        GenerateEveryday();
    }

    private void GenerateEveryday() => PlaylistsController.FillOnePlaylist(60, Everyday);

    private static void FillPlaylistsSection(ObservableCollection<Playlist> toFill, int startPercentageSongs)
    {
        List<string> localNames = PlaylistsController.PlaylistNames.ToList();

        int amount = Globals.Random.Next(2, 5); // amount of playlists in section
        for (int j = 0; j < amount; j++)
        {
            int index = Globals.Random.Next(0, localNames.Count);
            toFill.Add( new Playlist( $"{localNames[index]} #{_localCounts[index] + 1}" ) );
            _localCounts[index]++;

            PlaylistsController.FillOnePlaylist(startPercentageSongs, toFill[j]);
        }
    }
}
