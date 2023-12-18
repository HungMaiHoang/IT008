using Music_Player.Models;
using Music_Player.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            if(Playlist.Songs.Any(entity => entity.Path == SelectedSong.Path))
            {
                MessageBox.Show("Đã tồn tại bài hát trong playlist");
                return;
            }    
            Playlist.Songs.Add(SelectedSong);
        //    NavigationVM.Instance.LoadedCommand.Execute(this);
            NavigationVM.SongEntities.SaveChanges();
            //load ui
            NavigationVM.Instance.CurSongs.Add(SelectedSong);
            NavigationVM.Instance.TotalSong = NavigationVM.Instance.CurSongs.Count;
            var time = NavigationVM.Instance.CurPlaylist.Songs.ToList();
            NavigationVM.Instance.TotalTime = (long)time.Sum(c => c.Duration.TotalMinutes);
        }

        private void ExcuteClose(object obj)
        {
            CloseAction?.Invoke();
        }
    }
}
