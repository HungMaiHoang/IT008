using Music_Player.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Music_Player.Windows
{
    /// <summary>
    /// Interaction logic for VideoWindow.xaml
    /// </summary>
    public partial class VideoWindow : Window
    {
        public VideoWindow(string videoFilePath, VideoVM viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            mediaElement.Source = new Uri(videoFilePath, UriKind.RelativeOrAbsolute);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Đặt vị trí khởi đầu của cửa sổ video ở phía dưới màn hình
            Top = SystemParameters.WorkArea.Height;

            // Kích hoạt hiệu ứng trượt từ dưới lên khi cửa sổ được load
            Storyboard slideInAnimation = Resources["SlideInAnimation"] as Storyboard;
            if (slideInAnimation != null)
            {
                BeginStoryboard(slideInAnimation);
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Đóng cửa sổ khi video kết thúc
            Close();
        }
    }
}
