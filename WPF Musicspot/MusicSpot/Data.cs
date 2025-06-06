﻿using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Policy;
using System.Linq.Expressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using MySqlX.XDevAPI.Common;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore.Storage;
using ImageMagick;
using MusicSpot.Classes;
using System.Security.Cryptography;
using System.Text;

namespace MusicSpot
{
    // SQL ENTITYFRAMEWORK CORE
    //_________________________________________________________________________________
    public class Data: DbContext
    {
        const int KeySize = 64;
        const int Iterations = 350000;

        public DbSet<release> release { get; set; }

        public DbSet<UserAccount> account { get; set; }

        public DbSet<Song> song { get; set; }

        public DbSet<Genretype> genretype { get; set; }

        public DbSet<Product> product { get; set; }

        public DbSet<SubgenreType> subgenretype { get; set; }

        public DbSet<GenreObject> genreobject { get; set; }

        public DbSet<Wishlist> wishlist { get; set; }

        public DbSet<Likedlist> likedlist { get; set; }

        public DbSet<Purchase> purchase { get; set; }

        public DbSet<PurchaseProduct> purchaseproduct { get; set; }

        private string connectionstring = "datasource = 127.0.0.1;" +
            "port = 3307;" +
            "username = root;" +
            "password = ;" +
            "database = musicspot;";

            //"Server=127.0.0.1;Port=3306;Database=musicspot;Uid=root;Pwd=D1t1s33nP4sw00rd;";

        /*"datasource = 127.0.0.1;" +
        "port = 3307;" +
        "username = root;" +
        "password = ;" +
        "database = musicspot;";*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionstring);
        }
        public bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromHexString(parts[0]);
            byte[] hash = Convert.FromHexString(parts[1]);

            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            byte[] hashToCheck = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                Iterations,
                hashAlgorithm,
                hash.Length);

            return CryptographicOperations.FixedTimeEquals(hash, hashToCheck);
        }
        public string HashPassword(string password)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            byte[] salt = RandomNumberGenerator.GetBytes(KeySize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                Iterations,
                hashAlgorithm,
                KeySize);

            return $"{Convert.ToHexString(salt)}.{Convert.ToHexString(hash)}";
        }

        public void testconnection()
        {
            using (var context = new Data())
            {
                if (context.Database.CanConnect())
                {
                    MessageBox.Show("Succes");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }
        public List<release> GetAllReleases()
        {
            using (var context = new Data())
            {
                return context.release.ToList();
            }
        }
        public List<UserAccount> GetAllUsers()
        {
            return account.ToList();
        }
        public UserAccount IDLookup()
        {
            Data d = new Data(); List<UserAccount> allusers = d.GetAllUsers();
            foreach (UserAccount UA in allusers)
            {
                if (UA.AccountID == AccountID.AI)
                {
                    return UA;
                }
            }
            return null;
        }
        public Dictionary<string, string> PEN(int ID) //PEN = Password, Email, Name
        {
            Dictionary<string, string> PEN = new Dictionary<string, string>();
            using (var context = new Data())
            {
                var A = context.account.Where(current_ID => current_ID.AccountID == ID);
                foreach(UserAccount UA in A)
                {
                    PEN.Add("Password", UA.Password);
                    PEN.Add("Username", UA.Name);
                    PEN.Add("Email", UA.Email);
                    break;
                }
                return PEN;
            }
        }
        public BitmapImage pfp(int check = 0)
        {
            if(check == 0)
            {
                int accountid = AccountID.AI;
                using (var context = new Data())
                {
                    var A = context.account.FirstOrDefault(a => a.AccountID == AccountID.AI);
                    UserAccount UA = (UserAccount)A;
                    byte[] bytearray = UA.Profilepic;
                    try
                    {
                        using (MemoryStream Stream = new MemoryStream(bytearray))
                        {
                            using (MemoryStream stream = new MemoryStream(bytearray))
                            {
                                BitmapImage bitmap = new BitmapImage();
                                bitmap.BeginInit();
                                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                bitmap.StreamSource = stream;
                                bitmap.EndInit();
                                return bitmap;
                            }
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            else
            {
                int accountid = check;
                using (var context = new Data())
                {
                    var A = context.account.FirstOrDefault(a => a.AccountID == accountid);
                    UserAccount UA = (UserAccount)A;
                    byte[] bytearray = UA.Profilepic;
                    try
                    {
                        using (MemoryStream Stream = new MemoryStream(bytearray))
                        {
                            using (MemoryStream stream = new MemoryStream(bytearray))
                            {
                                BitmapImage bitmap = new BitmapImage();
                                bitmap.BeginInit();
                                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                bitmap.StreamSource = stream;
                                bitmap.EndInit();
                                return bitmap;
                            }
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
        public void DeleteAccount(int ID)
        {
            using (var context = new Data())
            {
                Data d = new Data();
                Navigation n = new Navigation();
                int deleteaccountID = ID;

                UserAccount DeleteUA = d.account.FirstOrDefault(x => x.AccountID == deleteaccountID);
                List<Wishlist> DeleteWishlist = d.wishlist.Where(y => y.AccountID == DeleteUA.AccountID).ToList();
                List<Purchase> DeletePurchase = d.purchase.Where(z => z.AccountID == DeleteUA.AccountID).ToList();
                List<Likedlist> DeleteLikedlist = d.likedlist.Where(q => q.AccountID == DeleteUA.AccountID).ToList();

                d.wishlist.RemoveRange(DeleteWishlist);
                d.purchase.RemoveRange(DeletePurchase);
                d.likedlist.RemoveRange(DeleteLikedlist);
                d.SaveChanges();

                d.account.Remove(DeleteUA);
                d.SaveChanges();
            }
        }
        public List<Song> songlist(int ReleaseID)
        {
            using (var context = new Data())
            {
                var songs = context.song.Where(s => s.ReleaseID == ReleaseID).ToList();
                return songs;
            }
        }
        /*public void ReleaseDel(string releasename)
        {
            using (var context = new Data())
            {
                release DeleteRelease = context.release.FirstOrDefault(r => r.ReleaseName == releasename);
                MessageBoxResult result = MessageBox.Show("Commander, are you sure you want to delete this release?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if(DeleteRelease != null)
                    {
                        int DelID = DeleteRelease.ReleaseID;

                        List<Product> products = context.product.Where(p => p.ReleaseID == DelID).ToList();
                        List<Song> songs = context.song.Where(x => x.ReleaseID == DeleteRelease.ReleaseID).ToList();
                        context.product.RemoveRange(products);
                        context.song.RemoveRange(songs);

                        context.SaveChanges();

                        context.release.Remove(DeleteRelease);

                        context.SaveChanges();

                        MessageBox.Show("The release has been successfully destroyed", "Successfull deletion", MessageBoxButton
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete non-existing release", "release not found");
                    }
                }
            }
        }*/
        public byte[] GetDefaultPfp()
        {
            string url = "https://i.postimg.cc/fRn91Yp2/defaultpfp.png";
            using (WebClient client = new WebClient())
            {
                return client.DownloadData(url);
            }
        }

        public byte[] GetDefaultCover()
        {
            string url = "https://i.postimg.cc/mDQmrbMc/Album.jpg";
            using (WebClient client = new WebClient())
            {
                return client.DownloadData(url);
            }
        }
        public byte[] PFP(bool isAlbum)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    string selectedFileName = dlg.FileName;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(selectedFileName);
                    bitmap.EndInit();
                    byte[] stuff = File.ReadAllBytes(selectedFileName);

                    return stuff;
                }
                catch
                {
                    MessageBox.Show("What the fuck did you do?");
                    if (isAlbum == false)
                    {
                        return GetDefaultPfp();
                    }
                    else
                    {
                        return GetDefaultCover();
                    }
                }
            }
            else
            {
                return GetDefaultPfp();
            }
        }
        public List<Genretype> GetGenretype()
        {
            using (var context = new Data())
            {
                return context.genretype.ToList();
            }
        }
        public List<Product> GetProductsByID(int RelID)
        {
            using (var context = new Data())
            {
                List<Product> products = context.product.Where(p => p.ReleaseID == RelID).ToList();
                return products;
            }
        }

        public void AddToWishlist(int accountID, int productID)
        {
            if (AccountID.AI != 0)
            {
                using (var context = new Data())
                {
                    bool ProductExists = context.wishlist.Any(w => w.AccountID == accountID & w.ProductID == productID);
                    if (ProductExists == true)
                    {
                        MessageBox.Show("Item has already been added to your wishlist");
                    }
                    else
                    {
                        Wishlist toAdd = new Wishlist(accountID, productID);
                        context.wishlist.Add(toAdd);
                        context.SaveChanges();
                        MessageBox.Show("Product has been added to your wishlist");
                    }
                }
            }
            else
            {
                MessageBox.Show("Log in to add products to your wishlist", "Account access required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public List<Product> GetWishlist(int accountID)
        {
            using (var context = new Data())
            {
                List<Product> wishlist = new List<Product>();
                List<int> productIDs = context.wishlist.Where(w => w.AccountID == accountID).Select(w => w.ProductID).ToList();
                foreach (int ID in productIDs)
                {
                    wishlist.Add(context.product.FirstOrDefault(p => p.ProductID == ID));
                }
                return wishlist;
            }
        }
        public int FindPurchase(int accountID)
        {
            using(var context = new Data())
            {
                int purchaseID = context.purchase
                    .Where(p => p.Processed == false && p.AccountID == accountID)
                    .Select(p => (int)p.PurchaseID).FirstOrDefault();
                return purchaseID;
            }
        }
        public void AddProductToCart(int accountID, int productID)
        {
            if (AccountID.AI != 0)
            {
                using (var context = new Data())
                {
                    int purchaseID = FindPurchase(accountID);
                    bool puchraseExist = context.purchaseproduct.Any(x => x.PurchaseID == purchaseID & x.ProductID == productID);
                    if (puchraseExist == true)
                    {
                        MessageBox.Show("Item has already been added to your shoppingcart");
                    }
                    else
                    {
                        if (purchaseID == 0)
                        {
                            Purchase purchase = new Purchase(false, accountID);
                            context.purchase.Add(purchase);
                            context.SaveChanges();

                            purchaseID = purchase.PurchaseID;

                        }

                        PurchaseProduct pp = new PurchaseProduct(purchaseID, productID);
                        context.purchaseproduct.Add(pp);
                        context.SaveChanges();
                        MessageBox.Show("Product has been added to your shoppingcart");
                    }
                }
            }
            else
            {
                MessageBox.Show("Log in to add products to your cart", "Account access required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public List<Product> GetShoppingCart(int accountID)
        {
            using (var context = new Data())
            {
                List<Product> shoppingCart = new List<Product>();
                int purchaseID = FindPurchase(accountID);
                if (purchaseID == 0)
                {
                    List<Product> empty = new List<Product>();
                    return empty;
                }

                List<int> productIDs = context.purchaseproduct
                    .Where(pp => pp.PurchaseID == purchaseID)
                    .Select(pp => (int)pp.ProductID).ToList();
                foreach (int pID in productIDs)
                {
                    shoppingCart.Add(context.product.FirstOrDefault(p => p.ProductID == pID));
                }
                return shoppingCart;
            }
        }
        public double ConfirmTransaction(int accountID)
        {
            int purchaseID = FindPurchase(accountID);
            using (var context = new Data())
            {
                List<Product> shoppingCart = GetShoppingCart(accountID);
                double paid = 0;
                foreach (Product sc in shoppingCart)
                {
                    var product = context.product.FirstOrDefault(p => p.ProductID == sc.ProductID);
                    paid += sc.Price;
                    product.Stock--;
                    context.SaveChanges();
                }
                DateTime date = DateTime.Now;

                Purchase transaction = context.purchase.FirstOrDefault(p => p.PurchaseID == purchaseID);
                transaction.Processed = true;
                transaction.Paid = paid;
                transaction.Date = date;
                context.SaveChanges();
                return paid;
            }
        }

        public void RemoveFromCart(int accountID, int productID)
        {
            int purchaseID = FindPurchase(accountID);
            using (var context = new Data())
            {
                var item = context.purchaseproduct.FirstOrDefault(pp => pp.PurchaseID == purchaseID && pp.ProductID == productID);
                if (item != null)
                {
                    context.purchaseproduct.Remove(item);
                    context.SaveChanges();
                }
            }
        }
        public double ToPay(int accountID)
        {
            int purchaseID = FindPurchase(accountID);
            using (var context = new Data())
            {
                List<Product> shoppingCart = GetShoppingCart(accountID);
                double paid = 0;
                foreach (Product p in shoppingCart)
                {
                    paid += p.Price;
                }

                return paid;
            }
        }
        public void AddToLikeList(int accountID, int releaseID)
        {
            if (AccountID.AI != 0)
            {
                using (var context = new Data())
                {
                    bool LikedlistExists = context.likedlist.Any(x => x.ReleaseID == releaseID & x.AccountID == accountID);
                    if (LikedlistExists == true)
                    {
                        MessageBox.Show("Item already added to your likedlist");
                    }
                    else
                    {
                        Likedlist toAdd = new Likedlist(accountID, releaseID);
                        context.likedlist.Add(toAdd);
                        context.SaveChanges();
                        MessageBox.Show("Release has been added to your likedlist");
                    }
                }
            }
            else
            {
                MessageBox.Show("Log in to add releases to your Likelist", "Account access required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public List<release> GetLikedList(int accountID)
        {
            using (var context = new Data())
            {
                List<release> likedList = new List<release>();
                List<int> productIDs = context.likedlist.Where(ll => ll.AccountID == accountID).Select(ll => ll.ReleaseID).ToList();
                foreach (int ID in productIDs)
                {
                    likedList.Add(context.release.FirstOrDefault(r => r.ReleaseID == ID));
                }
                return likedList;
            }
        }
        public void DeleteRelease(int releaseID)
        {
            using (var context = new Data())
            {
                List<int> productIDs = context.product.Where(p => p.ReleaseID == releaseID).Select(p => p.ProductID).ToList();
                List<Likedlist> likedLists = context.likedlist.Where(l => l.ReleaseID == releaseID).ToList();

                context.likedlist.RemoveRange(likedLists);
                context.SaveChanges();
                
                foreach (int pID in productIDs)
                {
                    List<Wishlist> wishlists = context.wishlist.Where(w => w.ProductID == pID).ToList();
                    context.wishlist.RemoveRange(wishlists);
                    context.SaveChanges();

                    List<PurchaseProduct> purchases = context.purchaseproduct.Where(pp => pp.ProductID == pID).ToList();
                    context.purchaseproduct.RemoveRange(purchases);
                    context.SaveChanges();

                    Product p = context.product.FirstOrDefault(p => p.ProductID == pID);
                    context.Remove(p);
                    context.SaveChanges();
                }

                List<Song> songs = context.song.Where(s => s.ReleaseID == releaseID).ToList();
                context.RemoveRange(songs);
                context.SaveChanges();

                release r = context.release.FirstOrDefault(r => r.ReleaseID == releaseID);
                context.Remove(r);
                context.SaveChanges();
            }
        }



        //SQL ENTITYFRAMEWORK CORE
        //___________________________________________________________________________________________


        //RAW SQL QUERRIES
        //___________________________________________________________________________________________
        public void TestQuerryConnection()
        {
            var connection = this.Database.GetDbConnection();

            MySqlCommand commandDatabase = new MySqlCommand("SELECT * FROM musicspot.release", (MySqlConnection)connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    string releasename = (string)reader["releasename"];
                    MessageBox.Show(releasename);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public int InsertGenre(int GenreID, int SubgenreID)
        {
            var connection = this.Database.GetDbConnection();

            MySqlCommand DuplicateCheck = new MySqlCommand("SELECT genre, subgenre FROM musicspot.genreobject;", (MySqlConnection)connection);

            MySqlCommand commandDatabase = new MySqlCommand("INSERT INTO musicspot.genreobject (genre, subgenre) VALUES (@GenreID, @SubgenreID);", (MySqlConnection)connection);
            commandDatabase.Parameters.AddWithValue("@GenreID", GenreID);
            commandDatabase.Parameters.AddWithValue("@SubgenreID", SubgenreID);
            try
            {
                connection.Open();
                commandDatabase.ExecuteNonQuery();

                long theid = commandDatabase.LastInsertedId;

                connection.Close();

                return (int)theid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                return -1;
            }
        }
        public List<string> SearchGenre(release r)
        {
            try
            {
                int releasegenre = r.GenreID;
                var connection = this.Database.GetDbConnection();

                connection.Open();

                List<string> genres = new List<string>();

                MySqlCommand commandDatabase = new MySqlCommand($"SELECT GT.Type FROM musicspot.genretype " +
                    $"AS GT INNER JOIN musicspot.genre AS GR " +
                    $"ON GT.ID = GR.genreObject WHERE GR.genreID = '{r.GenreID}';", (MySqlConnection)connection);

                //WHERE '{r.ReleaseID}' = MR.releaseID
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(reader["Type"].ToString());
                }
                return genres;
            }
            catch(Exception e)
            {
                MessageBox.Show($"Something went wrong: {e.Message}");
                return null;
            }
            
        }
        
        //RAW SQL QUERRIES
        //_____________________________________________________________________________________________________
    }
}

