using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot.Classes
{
    public class PurchaseProduct
    {
        public int PurchaseProductID { get; set; }
        public int PurchaseID { get; set; }
        public int ProductID { get; set; }

        public PurchaseProduct()
        {
            
        }
        public PurchaseProduct(int purchaseID, int productID)
        {
            PurchaseID = purchaseID;
            ProductID = productID;
        }
    }
}
