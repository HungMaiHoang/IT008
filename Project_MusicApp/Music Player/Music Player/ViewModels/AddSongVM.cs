using Microsoft.Win32;
using Music_Player.Models;
using Music_Player.Utilities;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
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

//SongEntities songEntities;
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
            ofd.Filter = "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;" +
            "*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;" +
            "*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;" +
            "*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV";
            var result = ofd.ShowDialog();
            if(result == true)
            {
                Song.Path = ofd.FileName;
                Song.Title = ofd.SafeFileName.ToString().Substring(0, ofd.SafeFileName.ToString().Length - 4);
            }
        }

        private bool CanAddSong(object arg)
        {
            return true;
        }
        
        private void AddSong(object obj)
        {
            AudioFileReader audioFileReader = new AudioFileReader(Song.Path);
            Song.Duration = audioFileReader.TotalTime.Duration();
            HomeVM.SongEntities.Songs.Add(Song);
            HomeVM.SongEntities.SaveChanges();
            HomeVM.Instance.SongList.Add(Song);
            Song = new Song();
        }
    }
}
