using Music_Player.Model;
using Music_Player.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Music_Player.ViewModel;

internal class PlaylistsPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObservableCollection<Playlist> UserPlaylists { get; set; }

    public ICommand ClearAllPlaylists => new Command(ClearPlaylists);

    public bool CanClear
    {
        get => _canClear;
        set
        {
            _canClear = value;
            RaisePropertyChanged();
        }
    }
    private bool _canClear;

    private Dictionary<string, string> _playlistsNamePath;

    public PlaylistsPageViewModel()
    {
        UserPlaylists = new ObservableCollection<Playlist>();

        _playlistsNamePath = new Dictionary<string, string>();

        ModelsController.SetViewModel(this);
        LoadPlaylistsFromFiles();
    }

    public void LoadPlaylistsFromFiles()
    {
        _playlistsNamePath.Clear();

        int nameCount = 1;
        UserPlaylists.Clear();

        string path = FileSystem.AppDataDirectory;

        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] Files = directoryInfo.GetFiles("*.list.txt");

        List<Playlist> userLists = new List<Playlist>();        
        foreach (FileInfo file in Files)
        {
            string name = $"Your #{nameCount}";
            nameCount++;

            Playlist toAdd = new Playlist(name)
            {
                SongTitles = File.ReadAllLines(file.FullName).ToList()
            };

            UserPlaylists.Add(toAdd);
            _playlistsNamePath.Add(name, file.FullName);
        }

        foreach (Playlist playlist in userLists) UserPlaylists.Add(playlist);

        CanClear = UserPlaylists.Count > 0;
    }

    public void ChoosePlaylist(string playlistName)
    {
        if (playlistName == null) return;

        Playlist newPlaylist = FindPlaylist(playlistName);

        Player.UpdateQueue(newPlaylist.SongTitles.ToList(), newPlaylist.PlaylistName);
    }

    public void OpenPlaylistPage(string playlistName)
    {
        if (playlistName == null) return;

        Playlist clicked = FindPlaylist(playlistName);

        PlaylistInfoPageOpen();
        UpdatePlaylistInfoPage(clicked);
    }

    private void UpdatePlaylistInfoPage(Playlist clickedOne)
    {
        ModelsController.UpdatePlaylistInfo(clickedOne);
    }

    private async void PlaylistInfoPageOpen() => await Shell.Current.GoToAsync(nameof(PlaylistInfoPage));

    private Playlist FindPlaylist(string playlistName)
    {
        int index = 0;
        for (int i = 0; i < UserPlaylists.Count; i++)
            if (UserPlaylists[i].PlaylistName.Equals(playlistName))
                index = i;

        return UserPlaylists[index];
    }

    private void ClearPlaylists()
    {
        UserPlaylists.Clear();
        string toDelete = "*.list.txt";

        string[] filesList = Directory.GetFiles(FileSystem.AppDataDirectory, toDelete);
        foreach (string file in filesList)
            File.Delete(file);

        _playlistsNamePath.Clear();

        CanClear = false;
    }

    public void RemovePlaylist(string playlistName)
    {
        if (playlistName == null) return;

        for (int i = 0; i < UserPlaylists.Count; i++)
        {
            if (UserPlaylists[i].PlaylistName.Equals(playlistName))
            {
                UserPlaylists.RemoveAt(i);
                File.Delete(_playlistsNamePath[playlistName]);

                _playlistsNamePath.Remove(playlistName);
            }
        }

        CanClear = UserPlaylists.Count > 0;
    }
}
