using Music_Player.Model;
using Music_Player.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Music_Player.ViewModel;

internal class PersonalPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObservableCollection<Song> FavouriteSongs { get; set; }

    public ICommand GoToAllSongs => new Command(AllSongsPageOpen);
    public ICommand GoToAuthors => new Command(AuthorsPageOpen);
    public ICommand GoToPlaylists => new Command(PlaylistsPageOpen);
    public ICommand ClearFavourites => new Command(Clear);

    public bool CanClear
    {
        get => _canClear;
        set
        {
            if (_canClear == value) return;

            _canClear = value;
            RaisePropertyChanged();
        }
    }
    private bool _canClear;

    public PersonalPageViewModel()
    {
        FavouriteSongs = new ObservableCollection<Song>();

        ModelsController.SetViewModel(this);
        SetFavourites();
    }

    public void SetFavourites()
    {
        FavouriteSongs.Clear();

        foreach (string title in FavouriteMusic.FavouriteTitles)
        {
            Song song = new Song(title);

            FavouriteSongs.Add(song);
        }

        FavouriteMusic.SaveCollectionToFile();
        CanClear = FavouriteSongs.Count > 0;
    }

    private async void AllSongsPageOpen() => await Shell.Current.GoToAsync(nameof(AllSongsPage));
    private async void PlaylistsPageOpen() => await Shell.Current.GoToAsync(nameof(PlaylistsPage));
    private async void AuthorsPageOpen() => await Shell.Current.GoToAsync(nameof(AuthorsPage));

    public void AddToFavourites(string path)
    {
        Song songToAdd = new Song(path);

        FavouriteMusic.FavouriteTitles.Add(path);

        SetFavourites();
    }

    public void ChooseTrack(string title)
    {
        if (title == null) return;

        PlaylistsController.ChooseTrackFromFavs(title);
    }

    public void RemoveTrackFromFavourites(string title)
    {
        if (title == null) return;

        string path = Converter.GetPathFromTitle(title);
        FavouriteMusic.FavouriteTitles.Remove(path);

        SetFavourites();
        ModelsController.UpdateQueue();
    }

    private void Clear()
    {
        FavouriteMusic.FavouriteTitles.Clear();
        SetFavourites();
        ModelsController.UpdateQueue();
    }
}
