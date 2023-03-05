using System.ComponentModel;

namespace Music_Player.Model;

internal class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Path { get; set; }
    public string Cover { get; set; }
    public string FavouriteImage 
    { 
        get 
            => FavouriteMusic.FavouriteTitles.Contains(Path) ? "heart_filled_crimson.png" : "heart_unfilled_crimson.png";
    }

    public Song(string path)
    {
        Path = path;

        Title = Converter.GetTitleFromPath(path);

        Artist = SongsInfo.AllAuthors[SongsInfo.AllSongs.IndexOf(Path)];
        Cover = SongsInfo.AllCovers[SongsInfo.AllSongs.IndexOf(Path)];
    }
}