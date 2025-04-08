using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class release
    {
        [Key]
        public int releaseID { get; set; }
        public string ReleaseName { get; set; }
        public string cover { get; set; }
        public releasetype releasetype { get; set; }
        public string artist { get; set; }
        public DateTime releasedate { get; set; }

    }
    public enum releasetype
    {
        Album,
        EP,
        Single,
        Other
    }
}
