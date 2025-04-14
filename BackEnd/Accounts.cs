using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class Account
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; }
        public byte[] ProfilePic { get; set; }
        public List<Product> Wishlist { get; set; }
        public List<Release> Likedlist { get; set; }
        public List<Purchase> Purchases { get; set; }
        public List<Release> Recommendations { get; set; }

        private Data data = new Data();

        public Account(string accountName, string email, string password, int isAdmin, byte[] profilePic)
        {
            AccountName = accountName;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
            ProfilePic = profilePic;
            Wishlist = new List<Product>();
            Likedlist = new List<Release>();
            Purchases = new List<Purchase>();
            Recommendations = new List<Release>();


            AccountID = data.CreateAccount(this);
        }

        public override string ToString()
        {
            return $"Account {AccountID} Made: {AccountName} - {Email} - {Password} - {IsAdmin} - {ProfilePic}";
        }

    }

    public class Product
    {
        public int ProductID { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public MediaType Type { get; set; }

        public Product(double price, int stock, MediaType mediaType)
        {
            Price = price;
            Stock = stock;
            Type = mediaType;
        }
    }

    public class Purchase
    {
        public int PurchaseID { get; set; }
        public DateTime Date { get; set; }
        public double Paid { get; set; }
        public Account Account { get; set; }
        public List<Product> Products { get; set; }

        public Purchase(DateTime date, double paid, Account account)
        {
            Date = date;
            Paid = paid;
            Account = account;
            Products = new List<Product>();
        }
    }

    public enum MediaType
    {
        Vinyl,
        CD,
        Cassette
    }
}
