using Music_Player.Models;
using Music_Player.Utilities;
using Music_Player.Views;
using Music_Player.Windows;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static Music_Player.Utilities.ObservableCollectionExtensions;
namespace Music_Player.ViewModels
{
    internal class NavigationVM : ViewModelBase
    {

        private bool isHome;
        public bool IsHome { get { return isHome; } set { isHome = value; OnPropertyChanged(nameof(IsHome)); } }
        private static NavigationVM _instance;
        public static NavigationVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationVM();
                }
                return _instance;
            }
        }

        private bool _isMute;
        public bool IsMute { get { return _isMute; } set { _isMute = value;OnPropertyChanged(nameof(IsMute)); } }
        private Visibility _buttonVisibility;
        public Visibility ButtonVisibility
        {
            get { return _buttonVisibility; }
            set
            {
                _buttonVisibility = value;
                OnPropertyChanged(nameof(ButtonVisibility));
            }
        }
        private string _curPlaylistName;

        private Models.Playlist _curPlaylist;

        private ObservableCollection<Models.Playlist> _listPlaylist;

        //private Song _song;
        private ObservableCollection<Song> _allSong;
        private ObservableCollection<Song> _curSongs;

        // SongEntities
        public static MyDatabaseEntities SongEntities;

        public string CurPlaylistName
        {
            get => _curPlaylistName;
            set
            {
                _curPlaylistName = value;
                OnPropertyChanged(nameof(CurPlaylistName));
            }
        }
        public Models.Playlist CurPlaylist
        {
            get => _curPlaylist;
            set
            {
                _curPlaylist = value;
                OnPropertyChanged(nameof(CurPlaylist));
            }
        }
        public ObservableCollection<Models.Playlist> ListPlaylist
        {
            get => _listPlaylist;
            set
            {
                _listPlaylist = value;
                OnPropertyChanged(nameof(ListPlaylist));
            }
        }
        //public Song Song
        //{
        //    get => _song;
        //    set
        //    {
        //        _song = value;
        //        OnPropertyChanged(nameof(Song));
        //    }
        //}
        public ObservableCollection<Song> AllSong
        {
            get => _allSong;
            set
            {
                _allSong = value;
                OnPropertyChanged(nameof(AllSong));
            }
        }
        public ObservableCollection<Song> CurSongs
        {
            get => _curSongs;
            set
            {
                _curSongs = value;
                LoadedCommand?.Execute(this);
                OnPropertyChanged(nameof(CurSongs));
            }
        }
        private Song selectedSong;
        public Song SelectedSong
        {
            get => selectedSong;
            set
            {
                selectedSong = value;
                OnPropertyChanged(nameof(SelectedSong));
            }
        }

        //private Uri _audioUri;
        //public Uri AudioUri
        //{
        //    get { return _audioUri; }
        //    set
        //    {       
        //        _audioUri = value;
        //        OnPropertyChanged(nameof(AudioUri));
        //    }
        //}
        private TimeSpan _duration;
        public TimeSpan _Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(_Duration));
            }
        }
        private string _nameSong;
        public string NameSong
        {
            get { return _nameSong; }
            set
            {
                _nameSong = value;
                OnPropertyChanged(nameof(NameSong));
            }
        }
        private string _artist;
        public string Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                OnPropertyChanged(nameof(Artist));
            }
        }

        // relate to naudio


        private double _currentTime = 0;
        public double CurrentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    OnPropertyChanged(nameof(CurrentTime));
                    VideoPosition = value; //TimeSpan.FromSeconds(value);
                    //audioFile.CurrentTime = TimeSpan.FromSeconds(CurrentTime);
                    //var mainWindow = Application.Current.MainWindow as MainWindow;

                    //// Tìm kiếm một thành phần UI bằng tên
                    //var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                    //myControl.Position = TimeSpan.FromSeconds(value);

                }
            }
        }

        private double _totalDuration;
        public double TotalDuration
        {
            get { return _totalDuration; }
            set
            {
                if (_totalDuration != value)
                {
                    _totalDuration = value;
                    OnPropertyChanged(nameof(TotalDuration));
                }
            }
        }
        //am thanh
        private double _volume = 1;
        public double Volume
        {
            get { return _volume; }
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    OnPropertyChanged(nameof(Volume));
                    // audioFile.Volume = (float)Volume;
                }
            }
        }
        private Song songPlaying;
        public Song SongPlaying
        {
            get { return songPlaying; }
            set { songPlaying = value; OnPropertyChanged(nameof(SongPlaying)); }
        }
        private AudioPlayer _audioPlayer;
        // So luong bai hat trong playlist   UI
        private int _totalSong;
        public int TotalSong { get { return _totalSong; } set { _totalSong = value; OnPropertyChanged(nameof(TotalSong)); } }
        // tong thoi gian cua 1 playlist    UI
        private long _totalTime;
        public long TotalTime { get { return _totalTime; } set { _totalTime = value; OnPropertyChanged(nameof(TotalTime)); } }
        private enum PlaybackState
        {
            Playing, Stopped, Paused
        }
        private PlaybackState _playbackState;
        private DispatcherTimer _timer;
        private MediaPlayer _MediaPlayer { get; set; }
        // visibility video
        private Visibility _videoVisibility = Visibility.Hidden;
        public Visibility VideoVisibility { get { return _videoVisibility; } set { _videoVisibility = value; OnPropertyChanged(nameof(VideoVisibility)); } }

        private double _videoPosition;
        public double VideoPosition
        {
            get => _videoPosition;
            set { _videoPosition = value; OnPropertyChanged(nameof(VideoPosition)); }
        }
        //   private WaveOutEvent waveOut;
        //    private AudioFileReader audioFile;
        public ICommand HomeCommand { get; set; }
        public ICommand PlaylistCommand { get; set; }
        public ICommand ShowCreatePlaylistWindowCommand { get; set; }
        public ICommand ShowAddSongToPlaylistWindowCommand { get; set; }
        public ICommand ShowAddSongWindowCommand { get; set; }
        public ICommand DeleteSongCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        public ICommand StartPlayCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand ValueChangedCommand { get; set; }
        public ICommand RewindToStartCommand { get; set; }
        public ICommand ControlMouseDownCommand { get; set; }
        public ICommand ControlMouseUpCommand { get; set; }
        public ICommand VolumeControlValueChangedCommand { get; set; }
        public ICommand ForwardToEndCommand { get; set; }
        public ICommand ShuffleCommand { get; set; }
        public ICommand StopPlayCommand { get; set; }
        public ICommand VideoVisibilityCommand { get; set; }
        public ICommand LoadedCommand { get; set; }
        public ICommand MuteCommand { get; set; }
        public ICommand EditNameCommand { get; set; }
        // Constructor
        public NavigationVM()
        {
            _playbackState = PlaybackState.Stopped;
            _instance = this;
            _MediaPlayer = new MediaPlayer();
            // Set up command
            ShowCreatePlaylistWindowCommand = new RelayCommand(ShowCreatePlaylistWindow);
            HomeCommand = new RelayCommand(Home);
            PlaylistCommand = new RelayCommand(LoadPlaylistToView);
            ShowAddSongWindowCommand = new RelayCommand(ShowAddSongWindow, CanShowWindow);
            ShowAddSongToPlaylistWindowCommand = new RelayCommand(ShowAddSongToPlaylistWindow);
            DeleteSongCommand = new RelayCommand(DeleteSong, CanDeleteSong);
            DeletePlaylistCommand = new RelayCommand(DeletePlaylist);
            StartPlayCommand = new RelayCommand(StartPlaySong, CanStartPlaySong);
            PauseCommand = new RelayCommand(PauseSong);
            ValueChangedCommand = new RelayCommand(ValueChange);
            RewindToStartCommand = new RelayCommand(RewindToStart, CanRewindToStart);
            ControlMouseDownCommand = new RelayCommand(ControlMouseDown, CanControlMouseDown);
            ControlMouseUpCommand = new RelayCommand(ControlMouseUp, CanControlMouseUp);
            VolumeControlValueChangedCommand = new RelayCommand(VolumeControlValueChanged, CanVolumeControlValueChanged);
            ForwardToEndCommand = new RelayCommand(ForwardToEnd, CanForwardToEnd);
            ShuffleCommand = new RelayCommand(Shuffle, CanShuffle);
            StopPlayCommand = new RelayCommand(StopPlayback, CanStopPlayback);
            VideoVisibilityCommand = new RelayCommand(OnVideoVisibility, CanOnVideoVisibility);
            LoadedCommand = new RelayCommand(LoadedIndex);
            MuteCommand = new RelayCommand(Mute);
            EditNameCommand = new RelayCommand(EditName);
            // Set up database
            SongEntities = new MyDatabaseEntities();
            //timer

            // Startup
            LoadAllPlaylist();
            //LoadAllSong();
            Home();
            Volume = 1;
            CurrentTime = 0;
            VideoPosition = 0;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(5);
            _timer.Tick += Timer_Tick;
        }
       
        private double _storeVolume;
        public double StoreVolume {  get { return _storeVolume; } set {  _storeVolume = value;OnPropertyChanged(nameof(StoreVolume)); } }
        private void Mute(object obj)
        {
          
            if (IsMute)
            {
                _audioPlayer?.SetVolume(0);
                StoreVolume = Volume;
                Volume = 0;
            }
            else
            {
                _audioPlayer?.SetVolume((float)StoreVolume);
                Volume = StoreVolume;
            }
            
        }
        public void LoadedIndex(object obj)
        {
            int index = 1;
            foreach(var item in CurSongs)
            {
                item.Index = index++;
            }
        }
        // function video
        private void OnVideoVisibility(object obj)
        {
            if (VideoVisibility == Visibility.Visible)
            {
                VideoVisibility = Visibility.Collapsed;
            }
            else VideoVisibility = Visibility.Visible;
        }
        private bool CanOnVideoVisibility(object obj)
        {
            var format = SongPlaying?.Path.Substring(SongPlaying.Path.Length - 3);
            if (format == "mp4")
            {
                return true;
            }
            else return false;
        }
        private void OpenVideoWindow(string videoFilePath)
        {

        }

        // function Naudio
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_playbackState == PlaybackState.Playing && _audioPlayer != null)
            {
                CurrentTime = _audioPlayer.GetPositionInSeconds();
                VideoPosition = _audioPlayer.GetPositionInSeconds();
            }
        }
        private void VolumeControlValueChanged(object p)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.SetVolume((float)Volume); // set value of the slider to current volume
            }
        }

        private bool CanVolumeControlValueChanged(object p)
        {
            return true;
        }
        private void ControlMouseDown(object p)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.Pause();
                var mainWindow = Application.Current.MainWindow as MainWindow;

                // Tìm kiếm một thành phần UI bằng tên
                var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                myControl.Pause();
            }
        }

        private void ControlMouseUp(object p)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.SetPosition(CurrentTime);
                _audioPlayer.Play(NAudio.Wave.PlaybackState.Paused, Volume);
                var mainWindow = Application.Current.MainWindow as MainWindow;

                // Tìm kiếm một thành phần UI bằng tên
                var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                myControl.Position = TimeSpan.FromSeconds(CurrentTime);
                myControl.Play();
            }
        }

        private bool CanControlMouseDown(object p)
        {
            if (_playbackState == PlaybackState.Playing)
            {
                return true;
            }
            return false;
        }

        private bool CanControlMouseUp(object p)
        {
            if (_playbackState == PlaybackState.Paused)
            {
                return true;
            }
            return false;
        }
        private void RewindToStart(object p)
        {
            //_audioPlayer.SetPosition(0); // set position to zero
            //var mainWindow = Application.Current.MainWindow as MainWindow;

            //// Tìm kiếm một thành phần UI bằng tên
            //var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
            //myControl.Position = TimeSpan.FromSeconds(0);
            if (StopPlayCommand.CanExecute(null))
            { StopPlayCommand.Execute(null); }
            _audioPlayer.PlaybackStopType = AudioPlayer.PlaybackStopTypes.PlaybackStoppedByUser;
            _audioPlayer.SetPosition(0);
            checkprevious = true;
        }
        private bool CanRewindToStart(object p)
        {
            if (_playbackState == PlaybackState.Playing)
            {
                return true;
            }
            return false;
        }
        private void ValueChange(object obj)
        {
            ////_MediaPlayer.Position = TimeSpan.FromSeconds(CurrentTime);
            //var mainWindow = Application.Current.MainWindow as MainWindow;

            //// Tìm kiếm một thành phần UI bằng tên
            //var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
            //myControl.Position = TimeSpan.FromSeconds(CurrentTime);

        }

        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
            }
        }
        private void StartPlaySong(object obj)
        {
            // _MediaPlayer.MediaOpened += (sender, e) => TotalDuration = _MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            //if (IsPlaying==false)
            //{
            //    waveOut.Pause();
            //    return;
            //}
            try
            {

            NameSong = SelectedSong.Title;
                Artist= SelectedSong.Artist;
            //  _Duration = SelectedSong.Duration;
            //    AudioUri = new Uri(SelectedSong.Path, UriKind.RelativeOrAbsolute);
            //    waveOut = new WaveOutEvent();
            //    audioFile = new AudioFileReader(AudioUri.ToString());
            //    waveOut.Init(audioFile);

            //    //  _Duration = SelectedSong.Duration.ToString(@"mm\:ss");
            //    //    _MediaPlayer.Open(AudioUri);
            //    //    _MediaPlayer.Play();
            //    waveOut.Play();
            //    TotalDuration = audioFile.TotalTime.TotalSeconds;
            //    DispatcherTimer timer = new DispatcherTimer();
            //    timer.Interval = TimeSpan.FromMilliseconds(50);
            //    timer.Tick += (sender, e) =>
            //    {
            //        if (audioFile != null)
            //        {
            //            CurrentTime = audioFile.CurrentTime.TotalSeconds;
            //        }
            //        // Cập nhật CurrentTime dựa trên giá trị thời gian hiện tại của MediaPlayer
            //        //if (_MediaPlayer.Source != null && _MediaPlayer.NaturalDuration.HasTimeSpan)
            //        //{
            //        //    CurrentTime = _MediaPlayer.Position.TotalSeconds;
            //        //}
            //    };
            //    timer.Start();

            // OpenVideoWindow(SelectedSong.Path);
            if (_audioPlayer != null && _playbackState == PlaybackState.Playing && SelectedSong != SongPlaying)
            {
                if (StopPlayCommand.CanExecute(null))
                { StopPlayCommand.Execute(obj); }
            }
            if (_playbackState == PlaybackState.Stopped )
            {
                _audioPlayer = new AudioPlayer(SelectedSong.Path, (float)Volume);
                _audioPlayer.PlaybackStopType = AudioPlayer.PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
                _audioPlayer.PlaybackPaused += _audioPlayer_PlaybackPaused;
                _audioPlayer.PlaybackResumed += _audioPlayer_PlaybackResumed;
                _audioPlayer.PlaybackStopped += _audioPlayer_PlaybackStopped;
                _Duration = TimeSpan.FromSeconds(_audioPlayer.GetLenghtInSeconds());
                TotalDuration = _audioPlayer.GetLenghtInSeconds();
                SongPlaying = SelectedSong;
                var mainWindow = Application.Current.MainWindow as MainWindow;

                // Tìm kiếm một thành phần UI bằng tên
                var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                myControl.Play();
            }
            if (SelectedSong == SongPlaying)
            {
                _audioPlayer.TogglePlayPause(Volume);
                //var mainWindow = Application.Current.MainWindow as MainWindow;

                //// Tìm kiếm một thành phần UI bằng tên
                //var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                //myControl.Pause();
            }
            _timer.Start();

            } catch (Exception ex) { MessageBox.Show("Không tìm thấy đường dẫn"); }

        }
        private bool CanStartPlaySong(object obj)
        {
            if (SelectedSong != null)
            {
                return true;
            }
            else return false;
        }

        private bool isRepeat=false;
        public bool IsRepeat { get { return isRepeat; } set { isRepeat = value;OnPropertyChanged(); } }
        private void _audioPlayer_PlaybackStopped()
        {
            _playbackState = PlaybackState.Stopped;
            CommandManager.InvalidateRequerySuggested();
            CurrentTime = 0;
            IsPlaying = false;

            if (_audioPlayer.PlaybackStopType == AudioPlayer.PlaybackStopTypes.PlaybackStoppedReachingEndOfFile)
            {
                if (IsRepeat)
                {
                    SelectedSong = SongPlaying;
                    //IsRepeat = false;
                    StartPlaySong(null);
                }
                else { SelectedSong = CurSongs.NextItem(SongPlaying); 
                    StartPlaySong(null);
                
                }
            } else if(_audioPlayer.PlaybackStopType == AudioPlayer.PlaybackStopTypes.PlaybackStoppedByUser&& checkprevious)
            {
                SelectedSong = CurSongs.PreviousItem(SongPlaying);
                checkprevious = false;
                    StartPlaySong(null);
            }
        }
        private void _audioPlayer_PlaybackResumed()
        {
            _playbackState = PlaybackState.Playing;
            IsPlaying = true;
        }

        private void _audioPlayer_PlaybackPaused()
        {
            _playbackState = PlaybackState.Paused;
            // isHome = false;
            IsPlaying = false;
        }
        private void StopPlayback(object p)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.PlaybackStopType = AudioPlayer.PlaybackStopTypes.PlaybackStoppedByUser;
                _audioPlayer.Stop();
                IsPlaying = false;
                _timer.Stop();
                var mainWindow = Application.Current.MainWindow as MainWindow;

                // Tìm kiếm một thành phần UI bằng tên
                var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                myControl.Stop();
            }
        }
        private bool CanStopPlayback(object p)
        {
            if (_playbackState == PlaybackState.Playing || _playbackState == PlaybackState.Paused)
            {
                return true;
            }
            return false;
        }
        private bool checkprevious = false;
        private void ForwardToEnd(object p)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.PlaybackStopType = AudioPlayer.PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
                _audioPlayer.SetPosition(_audioPlayer.GetLenghtInSeconds());
            }
        }
        private bool CanForwardToEnd(object p)
        {
            if (_playbackState == PlaybackState.Playing)
            {
                return true;
            }
            return false;
        }
        private ObservableCollection<Song> storeSong;
        public ObservableCollection<Song> StoreSong { get { return storeSong; } set { storeSong = value;OnPropertyChanged(nameof(StoreSong)); } }
        private bool isShuffle;
        public bool IsShuffle {  get { return isShuffle; } set { isShuffle = value;OnPropertyChanged(nameof(IsShuffle)); } }
        private void Shuffle(object p)
        {
            if (isShuffle)
            {
                StoreSong = new ObservableCollection<Song>(CurSongs.ToList());
                CurSongs = CurSongs.Shuffle();
            }
            else CurSongs = StoreSong;

        }
        private bool CanShuffle(object p)
        {
            if (_playbackState == PlaybackState.Stopped)
            {
                return true;
            }
            return false;
        }
        private void AudioFile_PositionChanged(object sender, EventArgs e)
        {
            //       CurrentTime = audioFile.CurrentTime.TotalSeconds;
        }
        private void PauseSong(object obj)
        {
            _MediaPlayer.Stop();

        }
        private void ShowAddSongToPlaylistWindow(object obj)
        {
            AddSongToPlaylistWindow addSongToPlaylistWindow = new AddSongToPlaylistWindow();
            addSongToPlaylistWindow.ShowDialog();
        }

        private Models.Playlist selected;
        public Models.Playlist Selected { get => selected; set { selected = value; OnPropertyChanged(nameof(Selected)); } }

        #region Command
        private void Home(object obj = null)
        {
            TotalSong = SongEntities.Songs.Count();
            var time = SongEntities.Songs.ToList();
            TotalTime = (long)time.Sum(c => c.Duration.TotalMinutes);
            IsHome = true;
            ButtonVisibility = Visibility.Visible;
            CurPlaylistName = "All Media";
            LoadAllSong();
            CurSongs = new ObservableCollection<Song>(AllSong.ToList());
            OnPropertyChanged(nameof(CurSongs));
        }
        private void LoadPlaylistToView(object obj)
        {
            Selected = obj as Models.Playlist;
            IsHome = false;
            ButtonVisibility = Visibility.Hidden;
            CurPlaylistName = (obj as Models.Playlist).Name;
            CurPlaylist = obj as Models.Playlist;
            var time = CurPlaylist.Songs.ToList();

            TotalTime = (long)time.Sum(song => song.Duration.TotalMinutes);
            TotalSong = CurPlaylist.Songs.Count();
            //CurSongs = new ObservableCollection<Song>(SongEntities.Playlists.Where(c=>c.PlaylistID==CurPlaylist.PlaylistID).SelectMany(c => c.Songs).ToList());
            if (CurPlaylist != null && CurPlaylist.PlaylistID > 0)
            {
                var songsInPlaylist = SongEntities.Playlists
                    .Where(playlist => playlist.PlaylistID == CurPlaylist.PlaylistID)
                    .SelectMany(playlist => playlist.Songs)
                    .ToList();
                CurSongs = new ObservableCollection<Song>(songsInPlaylist);
                OnPropertyChanged(nameof(CurSongs));
            }

        }
        public void LoadAllPlaylist()
        {
            ListPlaylist = new ObservableCollection<Models.Playlist>(SongEntities.Playlists);
        }

        private void ShowCreatePlaylistWindow(object obj)
        {
            CreatePlaylistWindow createPlaylistWindow = new CreatePlaylistWindow();
            createPlaylistWindow.ShowDialog();
        }

        private bool CanDeleteSong(object arg)
        {
            if (SelectedSong != null)
            {
                return true;
            }
            else return false;
        }
        private void DeleteSong(object obj)
        {
            if (/*CurSongs.SequenceEqual(AllSong)*/ IsHome)
            {
                SongEntities.Songs.Remove(SelectedSong);
                SongEntities.SaveChanges();
                NavigationVM.Instance.AllSong.Remove(SelectedSong);
                CurSongs = new ObservableCollection<Song>(AllSong.ToList());
                var time = SongEntities.Songs.ToList();
                TotalTime = (long)time.Sum(c => c.Duration.TotalMinutes);
                TotalSong = SongEntities.Songs.Count();
                foreach( var c in SongEntities.Playlists)
                {
                    c.TotalSong = c.Songs.Count();
                }
                // OnPropertyChanged(nameof(CurSongs));
            }
            else
            {
                var PlaylistToRemove = SongEntities.Playlists.Find(Selected.PlaylistID);
                var SongToRemove = SongEntities.Songs.Find(SelectedSong.SongID);
                if (PlaylistToRemove != null && SongToRemove != null)
                {
                    PlaylistToRemove.Songs.Remove(SelectedSong);
                   // SongEntities.Songs.Remove(SelectedSong);
                    SongEntities.SaveChanges();
                    CurSongs.Remove(SelectedSong);
                    
                    var time = CurPlaylist.Songs.ToList();

                    TotalTime = (long)time.Sum(song => song.Duration.TotalMinutes);
                    TotalSong = CurPlaylist.Songs.Count();
                    CurPlaylist.TotalSong = CurPlaylist.Songs.Count();
                    LoadedIndex(null);
                }
            }

        }
        private bool CanShowWindow(object obj)
        {
            return true;
        }
        private void ShowAddSongWindow(object obj)
        {
            if (IsHome)
            {

                AddSongWindow addSongWindow = new AddSongWindow();
                addSongWindow.ShowDialog();
            }
            else
            {
                AddSongToPlaylistWindow addSongToPlaylistWindow = new AddSongToPlaylistWindow();
                addSongToPlaylistWindow.ShowDialog();
            }
        }
        #endregion
        private void EditName(object obj)
        {
            if (Selected != null)
            {

            EditNamePlaylist editNamePlaylist = new EditNamePlaylist();
            editNamePlaylist.ShowDialog();
            }
        }
        private void LoadAllSong() //Read songs
        {
            AllSong = new ObservableCollection<Song>(SongEntities.Songs);
        }
        private void DeletePlaylist(object obj)
        {
            if (Selected != null)
            {

            SongEntities.Playlists.Remove(Selected);
            SongEntities.SaveChanges();
            ListPlaylist.Remove(Selected);
            }
            Home();
        }

        //full screen

    }
}
