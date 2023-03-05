using Music_Player.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Music_Player.ViewModel;

internal class PlaylistInfoPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObservableCollection<Song> Songs { get; set; }

    private Playlist _thisPlaylist;

    public string PlaylistTitle
    {
        get => _playlistTitle;
        set
        {
            if (_playlistTitle == value) return;

            _playlistTitle = value;
            RaisePropertyChanged();
        }
    }

    private string _playlistTitle;

    public PlaylistInfoPageViewModel()
    {
        Songs = new ObservableCollection<Song>();

        ModelsController.SetViewModel(this);
    }

    public void SetInformation(Playlist playlistToGiveInfo)
    {
        _thisPlaylist = playlistToGiveInfo;

        PlaylistTitle = _thisPlaylist.PlaylistName;

        foreach (string title in _thisPlaylist.SongTitles)
            Songs.Add(new Song(title));
    }

    public void ChooseTrack(string title)
    {
        if (title == null) return;

        Player.UpdateQueue(_thisPlaylist.SongTitles, _thisPlaylist.SongTitles.IndexOf(Converter.GetPathFromTitle(title)));
    }

    public static void AddToCurrentQueue(string title)
    {
        if (title == null) return;

        Player.AddToQueueEnd(Converter.GetPathFromTitle(title));
    }
    public static void AddNextToQueue(string title)
    {
        if (title == null) return;

        Player.AddNextToQueue(Converter.GetPathFromTitle(title));
    }
}
