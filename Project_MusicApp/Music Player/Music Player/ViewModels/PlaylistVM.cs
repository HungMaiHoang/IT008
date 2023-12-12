using Music_Player.Models;
using Music_Player.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Music_Player.ViewModels
{
    public class PlaylistVM : ViewModelBase
    {

        private static PlaylistVM instance =new PlaylistVM();
        public static PlaylistVM Instance { get => instance;
        }

        private Playlist _playlist = new Models.Playlist();
        public Playlist Playlist { get => _playlist; set { _playlist = value; OnPropertyChanged(nameof(Playlist)); } }
        public ICommand DebugLog { get; set; }

        public PlaylistVM(Playlist playlist) {
            Playlist = playlist;
            DebugLog = new RelayCommand(debugLog);
        }
        public PlaylistVM()
        {
            instance = this;
            DebugLog = new RelayCommand(debugLog);
        }

        private void debugLog(object obj)
        {
            MessageBox.Show(Playlist.Name);
        }
    }
}
