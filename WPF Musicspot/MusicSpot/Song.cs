using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class Song
    {
        public int SongID { get; set; }
        public string Name { get; set; }
        public int ReleaseID { get; set; }

        public Song()
        {
            
        }
        public Song(int songid, string name, int releaseid)
        {
            SongID = songid;
            Name = name;
            ReleaseID = releaseid;
        }
    }
}
