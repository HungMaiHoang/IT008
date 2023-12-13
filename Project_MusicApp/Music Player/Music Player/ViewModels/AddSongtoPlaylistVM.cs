using Music_Player.Models;
using Music_Player.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Music_Player.ViewModels
{
    public class AddSongToPlaylistVM:ViewModelBase
    {
        private ObservableCollection<Song> curSongs;

        public ObservableCollection<Song> CurSongs { get => curSongs; set { curSongs = value; OnPropertyChanged(nameof(CurSongs)); } }

        private Song selectedSong;

        public Song SelectedSong { get => selectedSong; set { selectedSong = value; OnPropertyChanged(nameof(SelectedSong)); } }

        private Playlist playlist;
        public Playlist Playlist { get => playlist; set { playlist = value; OnPropertyChanged(nameof(Playlist)); } }
        public Action CloseAction {  get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddSongCommand {  get; set; }

        public AddSongToPlaylistVM() {
            CurSongs = new ObservableCollection<Song>(NavigationVM.Instance.AllSong);
            CloseCommand = new RelayCommand(ExcuteClose);
            AddSongCommand = new RelayCommand(AddSong);
            Playlist = new Playlist();
            Playlist = NavigationVM.Instance.Selected;
        }

        private void AddSong(object obj)
        {
            Playlist.Songs.Add(SelectedSong);
            NavigationVM.SongEntities.SaveChanges();
            NavigationVM.Instance.CurSongs.Add(SelectedSong);
        }

        private void ExcuteClose(object obj)
        {
            CloseAction?.Invoke();
        }
    }
}
