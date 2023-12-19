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

        private Playlist _playlist = new Playlist();
        public Playlist Playlist { get => _playlist; set { _playlist = value; OnPropertyChanged(nameof(Playlist)); } }


        
        public AddPlaylistVM()
        {
            AddPlaylistCommand = new RelayCommand(AddPlaylist);
            Playlist.TotalSong = 0;
        }

        private void AddPlaylist(object obj)
        {
            if(NavigationVM.SongEntities.Playlists.Any(entity => entity.Name == Playlist.Name))
            {
                MessageBox.Show("Đã tồn tại playlist");
                Playlist = new Playlist();
                return;
            }    
           try
            {

            // Change Database
            NavigationVM.SongEntities.Playlists.Add(Playlist);
            NavigationVM.SongEntities.SaveChanges();

            // Change UI
            NavigationVM.Instance.ListPlaylist.Add(Playlist);
            Playlist = new Playlist();
            } catch (Exception ex) { MessageBox.Show("Vui lòng nhập tên playlist");}
        }
    }
}
