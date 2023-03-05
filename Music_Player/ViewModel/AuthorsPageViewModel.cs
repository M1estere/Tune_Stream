using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Music_Player.Model;

namespace Music_Player.ViewModel;

internal class AuthorsPageViewModel
{
    public ObservableCollection<AuthorBlock> Authors { get; set; }

    public AuthorsPageViewModel() 
    {
        Authors = new ObservableCollection<AuthorBlock>();

        Init();
    }

    private void Init()
    {
        FillAuthorsList();
        SortAuthors();

        InitializeSongsLists();
    }

    private void FillAuthorsList()
    {
        List<string> checkedAuthors = new List<string>();

        foreach (string author in SongsInfo.AllAuthors)
        {
            if (checkedAuthors.Contains(author)) continue;
            checkedAuthors.Add(author);

            Authors.Add(new AuthorBlock(author));
        }
    }

    private void InitializeSongsLists()
    {
        List<AuthorBlock> temp = Authors.ToList();

        Authors.Clear();
        for (int i = 0; i < temp.Count; i++)
        {
            AuthorBlock block = CheckAuthor(temp[i].Author);
            if (block != null) Authors.Add(block);
        }
    }

    private AuthorBlock CheckAuthor(string name)
    {
        List<AuthorBlock> temp = new List<AuthorBlock>();

        List<string> titlesToAdd = new List<string>();
        for (int i = 0; i < SongsInfo.AllAuthors.Count; i++)
        {
            if (name.Equals(SongsInfo.AllAuthors[i]) == false) continue;

            titlesToAdd.Add(SongsInfo.AllSongs[i]);
        }

        if (titlesToAdd.Count > 1)
        {
            AuthorBlock toAdd = new AuthorBlock(name)
            {
                Titles = titlesToAdd.ToList()
            };

            return toAdd;
        }

        return null;
    }

    private void CheckAuthorTracks(string name, int place)
    {
        List<string> titlesToAdd = new List<string>();
        for (int i = 0; i < SongsInfo.AllAuthors.Count; i++)
        {
            if (name.Equals(SongsInfo.AllAuthors[i]) == false) continue;

            titlesToAdd.Add(SongsInfo.AllSongs[i]);
        }

        if (titlesToAdd.Count > 1)
        {
            AuthorBlock toAdd = new AuthorBlock(name)
            {
                Titles = titlesToAdd.ToList()
            };

            Authors.RemoveAt(place);
            Authors.Insert(place, toAdd);
        } else
        {
            Authors.RemoveAt(place);
        }
    }

    private void SortAuthors()
    {
        List <string> authors = new List<string>();
        foreach (AuthorBlock block in Authors)
            authors.Add(block.Author);

        authors.Sort();
        Authors.Clear();

        foreach (string authorName in authors)
            Authors.Add(new AuthorBlock(authorName));
    }

    public void StartAuthorPlaylist(string name)
    {
        if (name == null) return;

        Playlist playlistToSet = new Playlist(name)
        {
            SongTitles = FindAuthor(name).Titles
        };

        Player.UpdateQueue(playlistToSet.SongTitles.ToList(), playlistToSet.PlaylistName);
    }

    private AuthorBlock FindAuthor(string name)
    {
        int k = 0;
        for (int i = 0; i < Authors.Count; i++)
        {
            if (Authors[i].Author != name) continue;

            k = i;
        }

        return Authors[k];
    }
}
