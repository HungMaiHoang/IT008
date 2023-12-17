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

namespace Music_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            DataContext = new NavigationVM();
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

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //  mediaElement.Position = TimeSpan.FromSeconds(TimeSlider.Value);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private bool isFullScreen = false;
        private double storedLeft, storedTop, storedWidth, storedHeight;

        // phóng to nhỏ cửa sổ
        private void ToggleFullScreen(object sender, RoutedEventArgs e)
        {
            if (isFullScreen)
            {
                // If already in full-screen mode, restore to normal size
                RestoreWindowSize();
            }
            else
            {
                // If not in full-screen mode, switch to full-screen
                GoFullScreen();
            }

            // Toggle the full-screen state
            isFullScreen = !isFullScreen;
        }

        private void GoFullScreen()
        {
            // Save the current window state
            storedLeft = Left;
            storedTop = Top;
            storedWidth = Width;
            storedHeight = Height;

            // Set the window to full-screen
            WindowState = WindowState.Normal;
            ResizeMode = ResizeMode.NoResize;

            // Maximize the window to cover the entire screen
            WindowState = WindowState.Maximized;
            // Topmost = true;
        }

        private void RestoreWindowSize()
        {
            // Restore the original window size and state
            WindowState = WindowState.Normal;
            ResizeMode = ResizeMode.CanResize;

            // Return to the original position and size
            Left = storedLeft;
            Top = storedTop;
            Width = storedWidth;
            Height = storedHeight;
            // Topmost = false;
        }
    }
}

