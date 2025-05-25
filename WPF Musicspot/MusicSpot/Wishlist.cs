using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class Wishlist
    {
        [Key]
        public int WishlistID { get; set; }
        public int AccountID { get; set; }
        public int ProductID { get; set; }
        public Wishlist()
        {
            
        }
        public Wishlist(int accountid, int productid)
        {
            AccountID = accountid;
            ProductID = productid;
        }
    }
}
