using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class Product
    {
        public int ProductID { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int Mediatype { get; set; }
        public int ReleaseID { get; set; }
        
        [NotMapped]
        public string MediaString
        {
            get
            {
                switch (Mediatype)
                {
                    case 1:
                        return "Vinyl";
                    case 2:
                        return "CD";
                    case 3:
                        return "Cassette";
                    default:
                        return "Media not defined";
                }
            }
        }
        public Product(double price, int stock, int mediatype, int releaseid)
        {
            Price = price;
            Stock = stock;
            Mediatype = mediatype;
            ReleaseID = releaseid;
        }
        public Product()
        {
            
        }
    }
}
