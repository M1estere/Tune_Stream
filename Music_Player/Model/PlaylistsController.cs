namespace Music_Player.Model;

internal static class PlaylistsController
{                                  
    public static List<Color> Colors = new List<Color>() { Color.Parse("#5FA8A4"), Color.Parse("#A33E27"), Color.Parse("#D69513"), // blue, red, orange,
                                                           Color.Parse("#139FD6"), Color.Parse("#3AB514"), Color.Parse("#9F20AB") };  // blue (lighter), green, pink

    public static List<string> PlaylistNames = new List<string>() { "Hot ", "Fresh ", "For You ", "Home ", "Mood ", "Sweet ", "Best ", "Top 2022 ", "Top 2021 ", "Top 2020 " };

    public static List<string> PlaylistImages = new List<string>() { "best_1.png", "best_2.png", "best_3.png", "music_1.png", "music_2.png" };

    public static List<string> PersonalPlaylistImages = new List<string>() { "personal_1.png", "personal_2.png", "personal_3.png" };

    public static void ChooseTrackFromAll(string title)
    {
        int currentTrackIndex = 0;
        for (int i = 0; i < SongsInfo.AllSongs.Count; i++)
        {
            string name = Converter.GetTitleFromPath(SongsInfo.AllSongs[i]);
            if (name.Equals(title)) currentTrackIndex = i;
        }

        Playlist newPlaylist = new Playlist("All Songs")
        {
            SongTitles = SongsInfo.AllSongs.ToList()
        };

        Player.UpdateQueue(newPlaylist.SongTitles, currentTrackIndex, "Favourites");
    }

    public static void ChooseTrackFromFavs(string title)
    {
        int currentTrackIndex = 0;
        for (int i = 0; i < FavouriteMusic.FavouriteTitles.Count; i++)
        {
            string name = Converter.GetTitleFromPath(FavouriteMusic.FavouriteTitles[i]);
            if (name.Equals(title)) currentTrackIndex = i;
        }

        Playlist newPlaylist = new Playlist("Favourites")
        {
            SongTitles = FavouriteMusic.FavouriteTitles.ToList()
        };

        Player.UpdateQueue(newPlaylist.SongTitles, currentTrackIndex, "Favourites");
    }

    public static void FillOnePlaylist(float startSongsPercentage, Playlist listToFill)
    {
        startSongsPercentage /= 100;

        List<string> titles = SongsInfo.AllSongs.ToList(); // all tracks temp
        int startSongsAmount = (int)Math.Ceiling(Convert.ToSingle(titles.Count) * startSongsPercentage); // at least some percentage of all tracks should be in playlist
        int length = Globals.Random.Next(startSongsAmount, titles.Count); // amount of tracks

        for (int i = 0; i < length; i++)
        {
            int randomIndex = Globals.Random.Next(0, titles.Count);
            string songTitle = titles[randomIndex];

            titles.Remove(songTitle);
            listToFill.AddSong(songTitle);
        }

        listToFill.SongTitles = listToFill.SongTitles.OrderBy(_ => Globals.Random.Next()).ToList(); // random sort
    }
}
