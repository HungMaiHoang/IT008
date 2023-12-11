using Music_Player.Models;
using Music_Player.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Music_Player.ViewModels
{
    public  class AddPlaylistVM : ViewModelBase
    {
        public ICommand AddPlaylistCommand {  get; set; }

        private Playlist playlist = new Playlist();
        public Playlist Playlistinstance { get => playlist; set { playlist = value; OnPropertyChanged(nameof(Playlistinstance)); } }


        
        public AddPlaylistVM()
        {
        AddPlaylistCommand = new RelayCommand(AddPlaylist);

        }

        private void AddPlaylist(object obj)
        {
            HomeVM.SongEntities.Playlists.Add(Playlistinstance);
            HomeVM.SongEntities.SaveChanges();
            NavigationVM.Instance.ListPlaylist.Add(Playlistinstance);
            Playlistinstance = new Playlist();
        }
    }
}
