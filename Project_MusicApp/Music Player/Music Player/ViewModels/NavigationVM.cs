using Music_Player.Models;
using Music_Player.Utilities;
using Music_Player.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Music_Player.ViewModels
{
    internal class NavigationVM : ViewModelBase
    {
        private static NavigationVM instance;
        public static NavigationVM Instance { get { return instance; } }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand PlaylistCommand { get; set; }
        public ICommand ShowCreatePlaylistWindowCommand { get; set; }
        private void Home(object obj) => CurrentView = new HomeVM();

        private Models.Playlist _playlist = new Models.Playlist();
        public Models.Playlist Playlist
        {
            get => _playlist;
            set
            {
                _playlist = value;
                OnPropertyChanged(nameof(Playlist));
            }
        }

        //private ObservableCollection<Button> _buttons;
        //public ObservableCollection<Button> Buttons { get => _buttons; set { _buttons = value; OnPropertyChanged(nameof(Buttons)); } }
        private ObservableCollection<Models.Playlist> _listPlaylist;
        public ObservableCollection<Models.Playlist> ListPlaylist
        {
            get => _listPlaylist;
            set
            {
                _listPlaylist = value;
                OnPropertyChanged(nameof(ListPlaylist));
            }
        }


        public NavigationVM()
        {
            instance= this;
            ShowCreatePlaylistWindowCommand = new RelayCommand(ShowCreatePlaylistWindow);
            HomeCommand = new RelayCommand(Home);
            PlaylistCommand = new RelayCommand(PlaylistView);
            
            // startup page
            CurrentView = new HomeVM();
            LoadPlaylist();
        }
        private Models.Playlist selected;
        public Models.Playlist Selected { get => selected; set {  selected = value; OnPropertyChanged(nameof(Selected)); } }

        private void PlaylistView(object obj) 
        {
            var CuView = obj as Models.Playlist;
            Selected = CuView;
            if(CurrentView is HomeVM)
                CurrentView = new PlaylistVM(CuView);
            else
            {
                PlaylistVM.Instance.Playlist = CuView;
            }
        }

        public void LoadPlaylist()
        {
            ListPlaylist = new ObservableCollection<Models.Playlist>(HomeVM.SongEntities.Playlists);
        }

        private void ShowCreatePlaylistWindow(object obj)
        {
            CreatePlaylistWindow createPlaylistWindow = new CreatePlaylistWindow();
            createPlaylistWindow.ShowDialog();
        }
    }
}
