using Music_Player.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Music_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MediaPlayer media = new MediaPlayer();
        public bool isPlaying;
        public MainWindow()
        {
            InitializeComponent();
            isPlaying = false;
            media.Open(new Uri("C:\\Users\\ACER\\Downloads\\abcd.mp3", UriKind.Absolute));
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.25);
            timer.Tick += ChangeTimeSlider;
            timer.Start();
        }

        private void ChangeTimeSlider(object sender, EventArgs e)
        {
            TimeSlider.Value = media.Position.TotalSeconds;
        }

        /// <summary>
        /// Collapse Menu tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_menutab_expanded_Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuTab.Visibility = Visibility.Collapsed;
            MenuTabCollapsed.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Expanded Menu tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_menutab_collapsed_Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuTabCollapsed.Visibility = Visibility.Collapsed;
            MenuTab.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Button_Play(object sender, RoutedEventArgs e)
        {
            TimeSlider.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
            VolumeSlider.Maximum = 1;
            VolumeSlider.Value = 0.5;
            if (isPlaying)
            {
                media.Pause();
                isPlaying = false;

            }
            else
            {
                media.Play();
                isPlaying = true;
            }
        }

        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
                media.Position = TimeSpan.FromSeconds(TimeSlider.Value);
 
        }
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var volumn = sender as Slider;
            media.Volume = volumn.Value;
        }
    }
}
