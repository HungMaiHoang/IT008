using Music_Player.Models;
using Music_Player.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Music_Player.ViewModels
{
    public class EditNameVM :ViewModelBase
    {
        private Playlist playlist;
        public Playlist Playlist { get => playlist; set { playlist = value; OnPropertyChanged(nameof(Playlist)); } }
        public ICommand EditNameCommand { get; set; }
        private string storeName;
        public EditNameVM()
        {
            Playlist = new Playlist();
            Playlist = NavigationVM.Instance.Selected;
            EditNameCommand = new RelayCommand(EditName);
            if (Playlist != null )
            {
            storeName = Playlist.Name;

            }
        }

        private void EditName(object obj)
        {
            if (NavigationVM.SongEntities.Playlists.Any(entity => entity.Name == Playlist.Name))
            {
                MessageBox.Show("Đã tồn tại playlist");
                Playlist.Name = storeName;
                return;
            }
            try
            {

                // Change Database
                NavigationVM.SongEntities.SaveChanges();

                // Change UI
            }
            catch (Exception ex) { MessageBox.Show("Vui lòng nhập tên playlist"); }
         
        }
    }
}
