using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot.Classes
{
    public class Likedlist
    {
        public int LikedlistID { get; set; }
        public int AccountID { get; set; }
        public int ReleaseID { get; set; }

        public Likedlist()
        {
            
        }
        public Likedlist(int accountID, int releaseID)
        {
            AccountID = accountID;
            ReleaseID = releaseID;
        }
    }
}
