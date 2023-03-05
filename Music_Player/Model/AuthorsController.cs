namespace Music_Player.Model;

internal class AuthorsController
{
    public static List<string> FillTitlesToBlock(string authorName)
    {
        List<string> list = new List<string>();

        for (int i = 0; i < SongsInfo.AllAuthors.Count; i++)
        {
            if (SongsInfo.AllAuthors[i].Equals(authorName)) 
                list.Add(SongsInfo.AllSongs[i]);
        }

        return list;
    }
}
