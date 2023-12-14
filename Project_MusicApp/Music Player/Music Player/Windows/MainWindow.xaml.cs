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

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
    }
}
