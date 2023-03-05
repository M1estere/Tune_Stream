using Music_Player.ViewModel;
using Plugin.Maui.Audio;

namespace Music_Player.Model;

internal static class Player
{
    public enum RepeatingState
    {
        NoRepeat, RepeatPlaylist, RepeatTrack
    }

    public static RepeatingState PlayerRepeatingState = RepeatingState.NoRepeat;

    private static QueuePageViewModel queuePage;

    // To Control Current Queue
    public static List<string> SongTitles = new List<string>();
    public static string CurrentTitle { get; set; }

    public static IAudioManager AudioManager;
    private static IAudioPlayer _player;

    private static int _currentTrackNumber = 0;
    private static bool _musicPaused = false;

    private static async void StartTrack()
    {
        _player = AudioManager.CreatePlayer( await FileSystem.OpenAppPackageFileAsync( SongTitles[_currentTrackNumber] ) );
        CurrentTitle = SongTitles[_currentTrackNumber];
    }

    public static double GetProgress() => _player.CurrentPosition / _player.Duration;
    public static double GetDuration() => _player.Duration;
    public static void SetTime(double value) => _player.Seek( value );

    public static int PreviousSong()
    {
        if (GetProgress() >= 0.2f)
        {
            SelectTrack(_currentTrackNumber);
            return _currentTrackNumber;
        }

        if (_currentTrackNumber == 0) SelectTrack(_currentTrackNumber);
        else
        {
            _currentTrackNumber -= 1;
            SelectTrack(_currentTrackNumber);
        }

        return _currentTrackNumber;
    }

    public static int NextSong()
    {
        if (_currentTrackNumber == SongTitles.Count - 1) _currentTrackNumber = -1;

        _currentTrackNumber++;

        SelectTrack(_currentTrackNumber);
       
        return _currentTrackNumber;
    }

    public static int NextSongAfterEnd()
    {
        if (PlayerRepeatingState == RepeatingState.RepeatTrack)
        {
            SelectTrack(_currentTrackNumber);
            return _currentTrackNumber;
        }

        if ((_currentTrackNumber == SongTitles.Count - 1) && PlayerRepeatingState == RepeatingState.RepeatPlaylist)
            _currentTrackNumber = -1;
        else if ((_currentTrackNumber == SongTitles.Count - 1) && PlayerRepeatingState != RepeatingState.RepeatPlaylist)
        {
            _currentTrackNumber = 0;
            SelectTrack(_currentTrackNumber);

            return -5;
        }

        _currentTrackNumber++;

        SelectTrack(_currentTrackNumber);

        return _currentTrackNumber;
    }

    public static int ChangePlayingState()
    {
        if (_player.IsPlaying == true)
        {
            PauseMusic();
            return 0;
        }
        else
        {
            UnpauseMusic();
            return 1;
        }
    }

    private static void PauseMusic()
    {
        _musicPaused = true;
        _player.Pause();
    }

    private static void UnpauseMusic()
    {
        _musicPaused = false;
        _player.Play();
    }

    public static void SelectTrack(int index)
    {
        _currentTrackNumber = index;
        _player?.Stop();

        StartTrack();
        if (_musicPaused == false) _player.Play();
    }

    public static void SetModel(QueuePageViewModel model)
    {
        queuePage = model;

        _player?.Stop();

        StartTrack();
        if (_musicPaused == false) _player.Play();
    }

    public static void SetCurrentTrackNumber(int newNumber) => _currentTrackNumber = newNumber;

    public static void AddToQueueEnd(string path)
    {
        if (CurrentTitle == path) return;

        string oldTrackTitle = CurrentTitle;

        if (SongTitles.Contains(path) == true)
            DeleteFromQueue(path);

        _currentTrackNumber = SongTitles.IndexOf(oldTrackTitle);

        SongTitles.Add(path);
        queuePage.UpdateUI(_currentTrackNumber);
    }

    public static void AddNextToQueue(string path)
    {
        if (CurrentTitle == path) return;

        int oldLength = SongTitles.Count;
        int index = _currentTrackNumber + 1;
        string oldTrackTitle = CurrentTitle;

        if (SongTitles.Contains(path) == true)
        {
            int trackIndex = SongTitles.IndexOf(path);
            DeleteFromQueue(path);

            index--;
            if (trackIndex > _currentTrackNumber) index++;
        }
        SongTitles.Insert(index, path);
        _currentTrackNumber = SongTitles.IndexOf(oldTrackTitle);

        queuePage.UpdateUI(_currentTrackNumber);
    }

    public static int DeleteFromQueue(string name)
    {
        int deletableIndex = 0;

        for (int i = 0; i < SongTitles.Count; i++)
            if (SongTitles[i].Equals(name)) deletableIndex = i;

        SongTitles.Remove(name);

        if (deletableIndex < _currentTrackNumber)
            _currentTrackNumber--;
            
        return _currentTrackNumber;
    }

    public static void UpdateQueue(List<string> titles, string playlistName)
    {
        SongTitles.Clear();
        SongTitles = titles.ToList();

        _currentTrackNumber = 0;
        SelectTrack(_currentTrackNumber);

        queuePage.SetCurrentTrackNumber(0);
        queuePage.UpdateUI(playlistName);
    }

    public static void UpdateQueue(List<string> titles, int startIndex)
    {
        SongTitles.Clear();
        SongTitles = titles.ToList();
        
        SelectTrack(startIndex);

        queuePage.SetCurrentTrackNumber(startIndex);
        queuePage.UpdateUI();
    }

    public static void UpdateQueue(List<string> titles, int startIndex, string name)
    {
        SongTitles.Clear();
        SongTitles = titles.ToList();

        SelectTrack(startIndex);

        queuePage.SetCurrentTrackNumber(startIndex);
        queuePage.UpdateUI(name);
    }

    public static void SetQueue(List<string> titles)
    {
        SongTitles.Clear();
        SongTitles = titles.ToList();

        _currentTrackNumber = 0;
        SelectTrack(_currentTrackNumber);
    }

    public static void JustSetQueue(List<string> titles)
    {
        SongTitles.Clear();
        SongTitles = titles.ToList();
    }

    public static string ChangeRepeatingState()
    {
        string returner = "";
        switch (PlayerRepeatingState)
        {
            case RepeatingState.NoRepeat:
                PlayerRepeatingState = RepeatingState.RepeatPlaylist;
                returner = "0";
                break;
            case RepeatingState.RepeatPlaylist:
                PlayerRepeatingState = RepeatingState.RepeatTrack;
                returner = "1";
                break;
            case RepeatingState.RepeatTrack:
                PlayerRepeatingState = RepeatingState.NoRepeat;
                returner = "";
                break;
            default:
                returner = "WTF";
                break;
        }

        return returner;
    }
}
