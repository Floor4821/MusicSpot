using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class account
    {
        [Key]
        public int accountID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int isadmin { get; set; }
        public string? profilepic { get; set; }
    }
}
