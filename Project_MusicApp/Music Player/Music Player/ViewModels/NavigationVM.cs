using Music_Player.Utilities;
using Music_Player.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Music_Player.ViewModels
{
    internal class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand PlaylistCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Playlist(object obj) => CurrentView = new PlaylistVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            PlaylistCommand = new RelayCommand(Playlist);
            // startup page
            CurrentView = new HomeVM();
        }
    }
}
