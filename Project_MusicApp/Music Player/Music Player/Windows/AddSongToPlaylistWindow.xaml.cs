﻿using Music_Player.ViewModels;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Music_Player.Windows
{
    /// <summary>
    /// Interaction logic for AddSongToPlaylistWindow.xaml
    /// </summary>
    public partial class AddSongToPlaylistWindow : Window
    {
        public AddSongToPlaylistWindow()
        {
            InitializeComponent();
            AddSongToPlaylistVM addSongToPlaylistVM = new AddSongToPlaylistVM();
            addSongToPlaylistVM.CloseAction = ()=> this.Close();
            DataContext = addSongToPlaylistVM;
        }
    }
}
