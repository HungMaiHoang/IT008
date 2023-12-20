using Microsoft.Win32;
using Music_Player.Models;
using Music_Player.Utilities;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Music_Player.ViewModels
{
    public class AddSongVM : ViewModelBase
    {

        
        public ICommand AddSongCommand {  get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand OpenFilePathCommand { get; set; }
        private Song song = new Song();

        public Song Song { get => song;
            set {
                song = value; 
                OnPropertyChanged(nameof(Song));
            }
        }


        public AddSongVM() {
            AddSongCommand = new RelayCommand(AddSong, CanAddSong);
            OpenFilePathCommand = new RelayCommand(OpenPath, CanOpenPath);
            ExitCommand = new RelayCommand(Exit, CanExit);
           // songEntities = new SongEntities();
        }

        private bool CanExit(object arg)
        {
            return true;
        }

        private void Exit(object obj)
        {
            
        }

        private bool CanOpenPath(object arg)
        {
            return true;
        }

        private void OpenPath(object obj)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "All Media Files|*.mp3;" +
            "*.mp4;" +"*.MP3;*.MP4;";
            
            var result = ofd.ShowDialog();
            if(result == true)
            {
                Song.Path = ofd.FileName;
                Song.Title = ofd.SafeFileName.ToString().Substring(0, ofd.SafeFileName.ToString().Length - 4);
                Song.Format = ofd.SafeFileName.ToString().Substring(ofd.SafeFileName.ToString().Length - 3,3);
            }
        }

        private bool CanAddSong(object arg)
        {
            return true;
        }
        
        private void AddSong(object obj)
        {
            if(Song.Path is null)
            {
                MessageBox.Show("Vui lòng nhập đường dẫn");
                return;
            }
            if(NavigationVM.SongEntities.Songs.Any(song => song.Path == Song.Path))
            {
                MessageBox.Show("Đã có bài hát trong app");
                Song = new Song();
                return;
            }
            try
            {
                AudioFileReader audioFileReader = new AudioFileReader(Song.Path);
                Song.Duration = audioFileReader.TotalTime.Duration();
                if (NavigationVM.Instance.AllSong.Count == 0)
                {
                    Song.Index = 1;
                }else Song.Index = NavigationVM.Instance.CurSongs.Last().Index + 1;
                NavigationVM.SongEntities.Songs.Add(Song);
                NavigationVM.SongEntities.SaveChanges();
                NavigationVM.Instance.AllSong.Add(Song);
                NavigationVM.Instance.CurSongs.Add(Song);
                //load ui
                NavigationVM.Instance.TotalSong = NavigationVM.Instance.CurSongs.Count;
                var time = NavigationVM.SongEntities.Songs.ToList();
                NavigationVM.Instance.TotalTime = (long)time.Sum(c => c.Duration.TotalMinutes);
                Song = new Song();
                NavigationVM.Instance.LoadedIndex(null);
            } catch (Exception ) { MessageBox.Show("Vui lòng nhập các thông tin cần thiết"); }
            
        }
    }
}
