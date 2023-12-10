using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Player.Models
{
    internal class Playlist
    {
        private int playlistID;
        private string titlePlaylist;

        public int PlaylistID { get => playlistID; set => playlistID = value; }
        public string TitlePlaylist { get => titlePlaylist; set => titlePlaylist = value; }
    }
}
