using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Player.Models
{
    internal class Song
    {
        private int songID;
        private string title;
        private string path;
        private TimeSpan duration;

        public int SongID { get => songID; set => songID = value; }
        public string Title { get => title; set => title = value; }
        public string Path { get => path; set => path = value; }
        public TimeSpan Duration { get => duration; set => duration = value; }
    }
}
