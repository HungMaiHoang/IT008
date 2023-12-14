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

        //private static PlaylistVM instance;
        //public static PlaylistVM Instance 
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new PlaylistVM();
        //        return instance;
        //    }
        //}

        private Playlist _playlist = new Models.Playlist();
        public Playlist Playlist { get => _playlist; set { _playlist = value; OnPropertyChanged(nameof(_playlist)); } }

        private int _id;
        private string _name;
        public int Id { get => _id; set { _id = value; OnPropertyChanged(nameof(_id)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(_name)); } }

        public ICommand DebugLog { get; set; }

        //public PlaylistVM(Playlist playlist) {
        //    Playlist = playlist;
        //    DebugLog = new RelayCommand(debugLog);
        //}
        public PlaylistVM()
        {
            Id = 0;
            Name = "Unknow";
            DebugLog = new RelayCommand(debugLog);
        }

        private void debugLog(object obj)
        {
            Name = "12345";
            MessageBox.Show(Playlist.Name);

        }

        public void ChangePlaylist(Models.Playlist curPlaylist)
        {
            if (curPlaylist == null)
            {

            }
            else
            {
                Playlist = curPlaylist;
                Id = curPlaylist.PlaylistID;
                Name = curPlaylist.Name;
            }
        }
    }
}
