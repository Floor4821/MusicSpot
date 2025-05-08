using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class SubgenreType
    {
        [Key]
        public int ID { get; set; }
        public string Type { get; set; }
        public SubgenreType()
        {
            
        }
    }
}
