using Music_Player.ViewModel;

namespace Music_Player.Model;

class ModelsController
{
    private static PlaylistsPageViewModel _playlistsModel;
    private static PersonalPageViewModel _personalModel;
    private static PlaylistInfoPageViewModel _playlistInfoModel;
    private static QueuePageViewModel _queuePageModel;

    public static void SetViewModel(PlaylistsPageViewModel model) => _playlistsModel = model;
    public static void SetViewModel(PersonalPageViewModel model) => _personalModel = model;
    public static void SetViewModel(PlaylistInfoPageViewModel model) => _playlistInfoModel = model;
    public static void SetViewModel(QueuePageViewModel model) => _queuePageModel = model;

    public static void UpdatePlaylistInfo(Playlist playlist)
    {
        if (_playlistInfoModel == null) return;

        _playlistInfoModel.SetInformation(playlist);
    }

    public static void UpdateUserPlaylists()
    {
        if (_playlistsModel == null) return;
        _playlistsModel.LoadPlaylistsFromFiles();
    }

    public static void UpdateFavourites()
    {
        if (_personalModel == null) return;
        _personalModel.SetFavourites();
    }

    public static void AddToFavourites(string path)
    {
        if (_personalModel == null)
        {
            FavouriteMusic.FavouriteTitles.Add(path);
            return;
        }
        _personalModel.AddToFavourites(path);
    }

    public static void UpdateQueue()
    {
        if (_queuePageModel == null) return;

        _queuePageModel.UpdateUI();
    }
}
