using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot.Classes
{
    public class Purchase
    {
        public int PurchaseID { get; set; }
        public DateTime Date { get; set; }
        public bool Processed { get; set; }
        public int AccountID { get; set; }

        public double? Paid { get; set; }
        public Purchase()
        {
            
        }
        public Purchase(DateTime date, bool processed, int accountID, double paid)
        {
            Date = date;
            Processed = processed;
            AccountID = accountID;
            Paid = paid;
        }
        public Purchase(bool processed, int accountID)
        {
            Processed = processed;
            AccountID = accountID;
        }
    }
}
