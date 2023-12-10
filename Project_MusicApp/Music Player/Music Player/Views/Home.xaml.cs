using Music_Player.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Music_Player.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        MediaPlayer media = new MediaPlayer();
        bool isPlaying;
        public Home()
        {
            InitializeComponent();
            DataContext = new HomeVM();
            isPlaying = false;
        }

        private void playlist_btnPlay_Click(object sender, RoutedEventArgs e)
        {
            media.Open(new Uri("C:\\Users\\ADMIN\\Downloads\\random music\\random-thoughts-20586.mp3", UriKind.Absolute));
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
    }
}
