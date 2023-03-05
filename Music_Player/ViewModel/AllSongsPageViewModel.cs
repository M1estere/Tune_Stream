using Music_Player.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Music_Player.ViewModel;

internal class AllSongsPageViewModel
{
    public ObservableCollection<Song> Songs { get; set; }
    public ObservableCollection<Song> SearchSongs { get; set; }

    public ICommand PerformSearch => new Command<string>(Search);

    public AllSongsPageViewModel()
    {
        Songs = new ObservableCollection<Song>();
        SearchSongs = new ObservableCollection<Song>();

        DrawSongs();
    }

    private void DrawSongs()
    {
        Songs.Clear();
        SearchSongs.Clear();

        for (int i = 0; i < SongsInfo.AllSongs.Count; i++)
            Songs.Add(new Song(SongsInfo.AllSongs[i]));

        foreach (Song song in Songs)
            SearchSongs.Add(song);
    }

    public void Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            SearchSongs.Clear();

            foreach (Song song in Songs)
                SearchSongs.Add(song);
            return;
        }

        List<Song> temp = new List<Song>();
        foreach (Song song in Songs)
        {
            // search by title and artist name
            if (song.Title.ToLower().Contains(query.ToLower()) == false && song.Artist.ToLower().Contains(query.ToLower()) == false) continue;
            temp.Add(song);
        }

        // var temp = Songs.Where(c => c.Title.ToLower().Contains(query.ToLower()));
        SearchSongs.Clear();

        foreach (Song song in temp)
            SearchSongs.Add(song);
    }

    public static void ChooseTrack(string title) 
    {
        if (title == null) return;

        PlaylistsController.ChooseTrackFromAll(title);
    }

    public static void AddNextToQueue(string title)
    {
        if (title == null) return;

        Player.AddNextToQueue(Converter.GetPathFromTitle(title));
    }
    public static void AddToCurrentQueue(string title)
    {
        if (title == null) return;

        Player.AddToQueueEnd(Converter.GetPathFromTitle(title));
    }

    public void AddToFavourites(string title)
    {
        if (title == null) return;

        FavouriteMusic.AddToFavourites(title);
        DrawSongs();
    }
}
