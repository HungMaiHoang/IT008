﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Music_Player.Utilities
{
    public class AudioPlayer
    {
        public enum PlaybackStopTypes
        {
            PlaybackStoppedByUser, PlaybackStoppedReachingEndOfFile
        }

        public PlaybackStopTypes PlaybackStopType { get; set; }

        private AudioFileReader _audioFileReader;

        private DirectSoundOut _output;

        private string _filepath;

        public event Action PlaybackResumed;
        public event Action PlaybackStopped;
        public event Action PlaybackPaused;

        public AudioPlayer(string filepath, float volume)
        {
            
            
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;

            _audioFileReader = new AudioFileReader(filepath) { Volume = volume };

            _output = new DirectSoundOut(200);
            _output.PlaybackStopped += _output_PlaybackStopped;

            var wc = new WaveChannel32(_audioFileReader);
            wc.PadWithZeroes = false;

            _output.Init(wc);
         
        }
        public void Play(PlaybackState playbackState, double currentVolumeLevel)
        {

            if (playbackState == PlaybackState.Stopped || playbackState == PlaybackState.Paused)
            {
                _output.Play();
            }

            _audioFileReader.Volume = (float)currentVolumeLevel;

            if (PlaybackResumed != null)
            {
                PlaybackResumed();
            }
        }

        private void _output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Dispose();
            if (PlaybackStopped != null)
            {
                PlaybackStopped();
            }
        }

        public void Stop()
        {
            if (_output != null)
            {
                _output.Stop();
            }
        }

        public void Pause()
        {
            if (_output != null)
            {
                _output.Pause();

                if (PlaybackPaused != null)
                {
                    PlaybackPaused();
                }
            }
        }

        public void TogglePlayPause(double currentVolumeLevel)
        {
            if (_output != null)
            {
                if (_output.PlaybackState == PlaybackState.Playing)
                {
                    Pause();
                    var mainWindow = Application.Current.MainWindow as MainWindow;

                    // Tìm kiếm một thành phần UI bằng tên
                    var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                    myControl.Pause();
                }
                else
                {
                    Play(_output.PlaybackState, currentVolumeLevel);
                    var mainWindow = Application.Current.MainWindow as MainWindow;

                    // Tìm kiếm một thành phần UI bằng tên
                    var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                    myControl.Play();
                }
            }
            else
            {
                Play(PlaybackState.Stopped, currentVolumeLevel);
                var mainWindow = Application.Current.MainWindow as MainWindow;

                // Tìm kiếm một thành phần UI bằng tên
                var myControl = mainWindow?.FindName("mediaElement") as MediaElement;
                myControl.Stop();
            }
        }

        public void Dispose()
        {
            if (_output != null)
            {
                if (_output.PlaybackState == PlaybackState.Playing)
                {
                    _output.Stop();
                }
                _output.Dispose();
                _output = null;
            }
            if (_audioFileReader != null)
            {
                _audioFileReader.Dispose();
                _audioFileReader = null;
            }
        }

        public double GetLenghtInSeconds()
        {
            if (_audioFileReader != null)
            {
                return _audioFileReader.TotalTime.TotalSeconds;
            }
            else
            {
                return 0;
            }
        }

        public double GetPositionInSeconds()
        {
            return _audioFileReader != null ? _audioFileReader.CurrentTime.TotalSeconds : 0;
        }

        public float GetVolume()
        {
            if (_audioFileReader != null)
            {
                return _audioFileReader.Volume;
            }
            return 1;
        }

        public void SetPosition(double value)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
            }
        }

        public void SetVolume(float value)
        {
            if (_output != null)
            {
                _audioFileReader.Volume = value;
            }
        }
    }
}
