using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class UserAccount
    {
        [Key]
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Isadmin { get; set; }
        public byte[]? Profilepic { get; set; }

        public UserAccount()
        {
            
        }
        public UserAccount(string name, string email, string password, int isadmin, byte[] profilepic = null)
        {
            Name = name;
            Email = email;
            Password = password;
            Isadmin = isadmin;
            Profilepic = profilepic;
        }
    }
}
