using Music_Player.Models;
using Music_Player.Utilities;
using Music_Player.Views;
using Music_Player.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Music_Player.ViewModels
{
    internal class NavigationVM : ViewModelBase
    {
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
                _curSongs= value;
                OnPropertyChanged(nameof(_curSongs));
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

        public ICommand HomeCommand { get; set; }
        public ICommand PlaylistCommand { get; set; }
        public ICommand ShowCreatePlaylistWindowCommand { get; set; }
        public ICommand ShowAddSongWindowCommand { get; set; }
        public ICommand DeleteSongCommand { get; set; }
        public ICommand DeletePlaylistCommand {  get; set; }
        // Constructor
        public NavigationVM()
        {
            _instance = this;

            // Set up command
            ShowCreatePlaylistWindowCommand = new RelayCommand(ShowCreatePlaylistWindow);
            HomeCommand = new RelayCommand(Home);
            PlaylistCommand = new RelayCommand(LoadPlaylistToView);
            ShowAddSongWindowCommand = new RelayCommand(ShowAddSongWindow, CanShowWindow);
            DeleteSongCommand = new RelayCommand(DeleteSong, CanDeleteSong);
            DeletePlaylistCommand = new RelayCommand(DeletePlaylist);
            // Set up database
            SongEntities = new MyDatabaseEntities();

            // Startup
            LoadAllPlaylist();
            //LoadAllSong();
            Home();
        }
        private Models.Playlist selected;
        public Models.Playlist Selected { get => selected; set {  selected = value; OnPropertyChanged(nameof(Selected)); } }

        #region Command
        private void Home(object obj = null)
        {
            ButtonVisibility = Visibility.Visible;
            CurPlaylistName = "All Media";
            LoadAllSong();
            CurSongs = new ObservableCollection<Song>(AllSong.ToList());
            OnPropertyChanged(nameof(CurSongs));
        }
        private void LoadPlaylistToView(object obj) 
        {
            ButtonVisibility = Visibility.Hidden;
            CurPlaylistName = (obj as Models.Playlist).Name;
            CurPlaylist = obj as Models.Playlist;
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
            return true;
        }
        private void DeleteSong(object obj)
        {
            if (CurSongs.SequenceEqual(AllSong))
            {
                SongEntities.Songs.Remove(SelectedSong);
                SongEntities.SaveChanges();
                NavigationVM.Instance.AllSong.Remove(SelectedSong);
                CurSongs = new ObservableCollection<Song>(AllSong.ToList());
                OnPropertyChanged(nameof(CurSongs));
            }
            else
            {
                var PlaylistToRemove = SongEntities.Playlists.Find(Selected.PlaylistID);
                var SongToRemove = SongEntities.Songs.Find(SelectedSong.SongID);
                if (PlaylistToRemove != null && SongToRemove != null)
                {
                    PlaylistToRemove.Songs.Remove(SelectedSong);
                    SongEntities.SaveChanges();
                    OnPropertyChanged(nameof(CurSongs));
                }
            }
            
        }
        private bool CanShowWindow(object obj)
        {
            return true;
        }
        private void ShowAddSongWindow(object obj)
        {
            AddSongWindow addSongWindow = new AddSongWindow();
            addSongWindow.Show();

        }
        #endregion

        private void LoadAllSong() //Read songs
        {
            AllSong = new ObservableCollection<Song>(SongEntities.Songs);
        }
        private void DeletePlaylist(object obj)
        {
            SongEntities.Playlists.Remove(Selected);
            SongEntities.SaveChanges();
            ListPlaylist.Remove(Selected);
        }
    }
}
