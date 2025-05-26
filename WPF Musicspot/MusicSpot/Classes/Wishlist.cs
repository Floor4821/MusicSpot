using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot.Classes
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
        public Wishlist(int accountID, int productID)
        {
            AccountID = accountID;
            ProductID = productID;
        }


    }
}
