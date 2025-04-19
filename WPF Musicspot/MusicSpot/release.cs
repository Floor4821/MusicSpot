using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class release
    {
        [Key]
        public int ReleaseID { get; set; }
        public string ReleaseName { get; set; }
        public byte[] Cover { get; set; }
        public releasetype Releasetype { get; set; }
        public string Artist { get; set; }
        public DateTime Releasedate { get; set; }
        public int GenreID { get; set; }
        public release()
        {
            
        }
        public release(int releaseid, string releasename,byte[] cover, releasetype rt, string artist, DateTime releasedate, int genreID)
        {
            ReleaseID = releaseid;
            ReleaseName = releasename;
            Cover = cover;
            Releasetype = rt;
            Artist = artist;
            Releasedate = releasedate;
            GenreID = genreID;
        }
    }
    public enum releasetype
    {
        Album,
        EP,
        Single,
        Other
    }
}
