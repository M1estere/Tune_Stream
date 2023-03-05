namespace Music_Player.Model;

internal class AuthorBlock
{
    public string Author { get; set; }

    public List<string> Titles { get; set; }
    public int TitlesCount { get => Titles.Count; }

    public Color BlockColor { get => PlaylistsController.Colors[Globals.Random.Next(0, PlaylistsController.Colors.Count)]; }
    public string AuthorCover { get; set; }

    public AuthorBlock(string name)
    {
        Titles = new List<string>();
        Author = name;
        AuthorCover = SongsInfo.AllCovers[SongsInfo.AllAuthors.IndexOf(Author)];

        FillTitles(name);
    }

    private void FillTitles(string name) => Titles = AuthorsController.FillTitlesToBlock(name);
}
