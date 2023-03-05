namespace Music_Player.Model;

internal static class Converter
{
    // convert 'ashes_of_dreams.mp3' to 'Ashes Of Dreams'
    public static string GetTitleFromPath(string title)
    {
        int index = title.IndexOf('.');
        if (index >= 0)
            title = title.Substring(0, index);

        string result = "";

        char prevChar = ' ';
        for (int i = 0; i < title.Length; i++)
        {
            if (Char.IsWhiteSpace(prevChar)) // first iteration
            {
                prevChar = title[i];
                result += Char.ToUpper(title[i]);
                continue;
            }
            if (prevChar == '_')
            {
                result += Char.ToUpper(title[i]);
                prevChar = title[i];
                continue;
            }

            if (title[i] != '_') result += title[i];
            else result += ' ';

            prevChar = title[i];
        }

        return result;
    }

    // covert 'Ashes Of Dreams' to 'ashes_of_dreams.mp3'
    public static string GetPathFromTitle(string title)
    {
        string result = title.ToLower();
        result = result.Replace(' ', '_');

        result += ".mp3";

        return result;
    }

    // convert 225 to 03:45
    public static string GetTimeFromSeconds(double seconds)
    {
        double minutes = seconds / 60;
        seconds %= 60;

        string result = $"{minutes.ToString("00")}:{seconds.ToString("00")}";

        return result;
    }
}
