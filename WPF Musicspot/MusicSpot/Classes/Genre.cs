using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot.Classes
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        public int Subgenre { get; set; }
        public int genreObject { get; set; }
        public Genre()
        {
            
        }
    }
}
