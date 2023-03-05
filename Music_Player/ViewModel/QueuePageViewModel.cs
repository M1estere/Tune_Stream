using Music_Player.Model;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Music_Player.ViewModel
{
    internal class QueuePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Song> Songs { get; set; }

        public ICommand Next => new Command<string>(NextSong);
        public ICommand ChangeState => new Command(ChangeSongState);
        public ICommand Previous => new Command(PreviousSong);
        public ICommand SavePlaylist => new Command(SavePlayList);
        public ICommand ChangePlayerRepeatingState => new Command(ChangeRepeatState);

        public Command<Song> DragOver { get; }
        public Command<Song> DragStarted { get; }

        public string CurrentTrackName
        {
            get => _currentTrackName;
            set
            {
                if (_currentTrackName == value) return;

                _currentTrackName = value;
                RaisePropertyChanged();
            }
        }
        public string CurrentTrackAuthor 
        {
            get => _currentTrackAuthor; 
            set 
            {
                if (_currentTrackAuthor == value) return;

                _currentTrackAuthor = value;
                RaisePropertyChanged(); 
            } 
        }
        public string CurrentTrackDuration
        {
            get => _currentTrackDuration;
            set
            {
                if (_currentTrackDuration == value) return;

                _currentTrackDuration = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentPlaylistName
        {
            get => _currentPlaylistName;
            set
            {
                if (_currentPlaylistName == value) return;

                _currentPlaylistName = value;
                RaisePropertyChanged();
            }
        }
        public string CurrentCover
        {
            get => _currentCover;
            set
            {
                if (_currentCover == value) return;

                _currentCover = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentRepeatState
        {
            get => _currentRepeatState;
            set
            {
                if (_currentRepeatState == value) return;

                _currentRepeatState = value;
                RaisePropertyChanged();
            }
        }

        public double CurrentProgressForSlider
        {
            get => _currentTrackProgressForSlider;
            set
            {
                if (_currentTrackProgressForSlider == value) return;

                _currentTrackProgressForSlider = value;
                RaisePropertyChanged();
            }
        }
        public string CurrentSecond
        {
            get => _currentSecond;
            set
            {
                if (_currentSecond == value) return;

                _currentSecond = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentGif
        {
            get => _currentGif;
            set
            {
                if (_currentGif == value) return;

                _currentGif = value;
                RaisePropertyChanged();
            }
        }
        public string CurrentPauseImage
        {
            get => _currentPauseImage;
            set
            {
                if (_currentPauseImage == value) return;

                _currentPauseImage = value;
                RaisePropertyChanged();
            }
        }

        private string _currentCover;
        private string _currentTrackName;
        private string _currentTrackAuthor;
        private string _currentTrackDuration;
        private double _currentTrackProgressForSlider;
        private string _currentSecond;
        private string _currentPlaylistName;

        private string _currentRepeatState;

        private string _currentGif;
        private string _currentPauseImage;

        private int _currentTrackNumber;

        private Song _songBeingDragged;

        public QueuePageViewModel(IAudioManager audioManager)
        {
            FavouriteMusic.LoadCollectionFromFile();
            ModelsController.SetViewModel(this);

            DragStarted = new Command<Song>((s) =>
            {
                _songBeingDragged = s;
            });

            DragOver = new Command<Song>((s) =>
            {
                if (s.Title == _songBeingDragged.Title) return;

                Songs.Move(Songs.IndexOf(_songBeingDragged), Songs.IndexOf(s));
            });

            Globals.FillAllSongs();

            Player.AudioManager = audioManager;
            
            Songs = new ObservableCollection<Song>();

            CurrentGif = "running.gif";
            CurrentPauseImage = "pause.png";

            InitializeSongs();
            StartFirstSong();

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                CurrentProgressForSlider = Player.GetProgress();
                CurrentSecond = Converter.GetTimeFromSeconds( CurrentProgressForSlider * GetDuration() );

                CheckTrackProgression();

                return true;
            });
        }

        private void CheckTrackProgression()
        {
            double currentSeconds = CurrentProgressForSlider * GetDuration();
            if (currentSeconds >= GetDuration() - 2) NextSong();
        }

        public static void SetTimeSpan(double value) => Player.SetTime(value);

        public static double GetDuration() => Player.GetDuration();

        private static void InitializeSongs()
        {
            Playlist playlist = new Playlist("Start List");

            PlaylistsController.FillOnePlaylist(10, playlist);

            Player.SetQueue(playlist.SongTitles);
        }

        private void StartFirstSong()
        {
            Player.SetModel(this);
            _currentTrackNumber = 0;

            UpdateUI();
        }

        private void PreviousSong()
        {
            _currentTrackNumber = Player.PreviousSong();

            UpdateUI();
        }

        private void ChangeSongState()
        {
            int pauseState = Player.ChangePlayingState();
            if (pauseState == 1)
                CurrentGif = "running.gif"; // unpaused
            else
                CurrentGif = "waiting.gif"; // paused
        }

        private void NextSong(string s)
        {
            _currentTrackNumber = Player.NextSong();

            UpdateUI();
        }

        private void NextSong()
        {
            _currentTrackNumber = Player.NextSongAfterEnd();

            if (_currentTrackNumber == -5)
            {
                ChangeSongState();
                _currentTrackNumber = 0;
            }

            UpdateUI();
        }

        private void ChangeRepeatState() => CurrentRepeatState = Player.ChangeRepeatingState();

        public void ChooseTrack(string title)
        {
            if (title == null) return;

            int songIndex = FindSong(title);

            if (_currentTrackNumber == songIndex) return;

            _currentTrackNumber = songIndex;

            Player.SelectTrack(_currentTrackNumber);

            UpdateUI();
        }

        private int FindSong(string title)
        {
            int songIndex = 0;
            for (int i = 0; i < Songs.Count; i++)
                if (Songs[i].Title.Equals(title)) songIndex = i;

            return songIndex;
        }

        public void RemoveTrack(string title)
        {
            if (title == null) return;

            if (title == Songs[_currentTrackNumber].Title) return;

            // find deletable track path
            string path = Songs[FindSong(title)].Path;

            _currentTrackNumber = Player.DeleteFromQueue(path);

            UpdateUI();
        }

        private void CreateSong(string title)
        {
            Song songToAdd = new Song(title);

            Songs.Add(songToAdd);
        }

        public void UpdateUI()
        {
            Songs.Clear();

            foreach (string title in Player.SongTitles) CreateSong(title);

            CurrentTrackName = Songs[_currentTrackNumber].Title.ToString();
            CurrentTrackAuthor = Songs[_currentTrackNumber].Artist.ToString();
            CurrentTrackDuration = Converter.GetTimeFromSeconds(GetDuration() - 2);

            CurrentCover = Songs[_currentTrackNumber].Cover;
        }

        public void UpdateUI(int currentTrackNumber)
        {
            Songs.Clear();
            _currentTrackNumber = currentTrackNumber;

            foreach (string title in Player.SongTitles) CreateSong(title);

            CurrentTrackName = Songs[_currentTrackNumber].Title.ToString();
            CurrentTrackAuthor = Songs[_currentTrackNumber].Artist.ToString();
            CurrentTrackDuration = Converter.GetTimeFromSeconds(GetDuration() - 2);

            CurrentCover = Songs[_currentTrackNumber].Cover;
        }

        public void UpdateUI(string playlistName)
        {
            Songs.Clear();

            foreach (string title in Player.SongTitles) CreateSong(title);

            CurrentTrackName = Songs[_currentTrackNumber].Title.ToString();
            CurrentTrackAuthor = Songs[_currentTrackNumber].Artist.ToString();
            CurrentTrackDuration = Converter.GetTimeFromSeconds(GetDuration() - 2);

            CurrentCover = Songs[_currentTrackNumber].Cover;

            CurrentPlaylistName = playlistName;
        }

        public void AddToFavourites(string title)
        {
            if (title == null) return;

            FavouriteMusic.AddToFavourites(title);

            UpdateUI();
        }

        public void SetCurrentTrackNumber(int number) => _currentTrackNumber = number;

        public void UpdateQueue()
        {
            List<string> newTitles = new List<string>();
            
            foreach (Song song in Songs) newTitles.Add(song.Path);

            Player.JustSetQueue(newTitles);

            // find track
            int index = FindSong(CurrentTrackName);

            SetCurrentTrackNumber(index);
            Player.SetCurrentTrackNumber(index);
        }

        private void SavePlayList()
        {
            string appDataPath = FileSystem.AppDataDirectory;
            
            string filename = $"{Path.GetRandomFileName()}.list.txt";

            string resPath = Path.Combine(appDataPath, filename);

            WriteToFile(resPath);

            ModelsController.UpdateUserPlaylists();
        }

        private static void WriteToFile(string path) => File.WriteAllLines(path, Player.SongTitles);
    }
}
