namespace Music_Player.Model;

internal class Playlist
{
    public List<string> SongTitles;

    public string PlaylistName { get; set; }
    public int SongsAmount { get => SongTitles.Count; }

    public Color PlaylistColor { get => PlaylistsController.Colors[Globals.Random.Next(0, PlaylistsController.Colors.Count)]; }

    public string Image { get => PlaylistsController.PlaylistImages[Globals.Random.Next(0, PlaylistsController.PlaylistImages.Count)]; }

    public string PersonalImage { get => PlaylistsController.PersonalPlaylistImages[Globals.Random.Next(0, PlaylistsController.PersonalPlaylistImages.Count)]; }

    public Playlist(string name)
    {
        SongTitles = new List<string>();
        PlaylistName = name;
    }

    public void AddSong(string title) => SongTitles.Add(title);
}
