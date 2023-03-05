namespace Music_Player.Model;

internal static class FavouriteMusic
{
    public static List<string> FavouriteTitles = new List<string>();

    private static string _favouritesFileName = "favourite_tracks.txt";

    public static void SaveCollectionToFile()
    {
        string directoryPath = FileSystem.AppDataDirectory;
        string resultPath = Path.Combine(directoryPath, _favouritesFileName);

        if (File.Exists(resultPath) == false)
            File.Create(resultPath);
        
        File.WriteAllLines(resultPath, FavouriteTitles);
    }

    public static void LoadCollectionFromFile()
    {
        FavouriteTitles.Clear();
        string directoryPath = FileSystem.AppDataDirectory;
        string resultPath = Path.Combine(directoryPath, _favouritesFileName);

        if (File.Exists(resultPath) == false || new FileInfo(resultPath).Length == 0) return;

        FavouriteTitles = File.ReadAllLines(resultPath).ToList();
    }

    public static void AddToFavourites(string title)
    {
        LoadCollectionFromFile();

        string path = Converter.GetPathFromTitle(title);

        if (FavouriteTitles.Contains(path) == true)
        {
            FavouriteTitles.Remove(path);
            SaveCollectionToFile();
            ModelsController.UpdateFavourites();
            return;
        }

        ModelsController.AddToFavourites(path);

        SaveCollectionToFile();
    }
}
