using Music_Player.Models;
using Music_Player.Utilities;
using Music_Player.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Music_Player.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        private static HomeVM instance;
        public static HomeVM Instance 
        {  get => instance; 
        }

        public ICommand ShowAddSongWindowCommand {  get; set; }
        public ICommand DeleteSongCommand { get; set; }

        private Song _song;
        public Song Song { get => _song; 
            set 
            {
                _song = value;
                OnPropertyChanged(nameof(Song));
            } 
        }
        
        private  ObservableCollection<Song> _songList;
        public  ObservableCollection<Song> SongList
        {
            get => _songList;
            set
            {
                _songList = value;
                OnPropertyChanged(nameof(SongList));
            }
        }

        private Song selectedSong;
        public Song SelectedSong { get => selectedSong; set { selectedSong = value; OnPropertyChanged(nameof(SelectedSong)); } }


        public static MyDatabaseEntities SongEntities;
        public HomeVM() 
        {
            instance = this;
            ShowAddSongWindowCommand = new RelayCommand(ShowAddSongWindow, CanShowWindow);
            SongEntities = new MyDatabaseEntities();
            LoadSong();
            DeleteSongCommand = new RelayCommand(DeleteSong, CanDeleteSong);
        }

        private bool CanDeleteSong(object arg)
        {
            return true;
        }

        private void DeleteSong(object obj)
        {
            SongEntities.Songs.Remove(SelectedSong);
            SongEntities.SaveChanges();
            SongList.Remove(SelectedSong);
        }

        private void LoadSong() //Read songs
        {
            SongList = new ObservableCollection<Song>(SongEntities.Songs);
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
        
    }
}
