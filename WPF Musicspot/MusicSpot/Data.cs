using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient; 
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
using System.Text.RegularExpressions;

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
            return release.ToList();
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
                using (var CONTEXT = new Data())
                {
                    var A = CONTEXT.account.FirstOrDefault(current_ID => current_ID.AccountID == AccountID.AI);
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
                using (var CONTEXT = new Data())
                {
                    var A = CONTEXT.account.FirstOrDefault(current_ID => current_ID.AccountID == accountid);
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
            using (var data = new Data())
            {
                var delete_me = data.account.FirstOrDefault(x => x.AccountID == ID);
                data.account.Remove(delete_me);
                data.SaveChanges();
            }
        }
        public List<Song> songlist(int ReleaseID)
        {
            using (var data = new Data())
            {
                var songs = data.song.Where(x => x.ReleaseID == ReleaseID).ToList();
                return songs;
            }
        }
        public void ReleaseDel(string releasename)
        {
            using (var data = new Data())
            {
                release DeleteRelease = data.release.FirstOrDefault(x => x.ReleaseName == releasename);
                MessageBoxResult result = MessageBox.Show("Commander, are you sure you want to delete this release?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    int DelID = DeleteRelease.ReleaseID;

                    List<Product> products = data.product.Where(x => x.ReleaseID == DelID).ToList();
                    foreach (Product p in products)
                    {
                        data.product.Remove(p);
                    }
                    data.SaveChanges();

                    data.release.Remove(DeleteRelease);

                    data.SaveChanges();

                    MessageBox.Show("Commander, the release has been successfully destroyed", "Successfull deletion", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        public byte[] PFP()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                byte[] stuff = File.ReadAllBytes(selectedFileName);

                return stuff;
            }
            else
            {
                return null;
            }
        }
        public List<Genretype> GetGenretype()
        {
            using (var data = new Data())
            {
                return data.genretype.ToList();
            }
        }
        public List<Product> GetProductsByID(int RelID)
        {
            using (var data = new Data())
            {
                List<Product> products = data.product.Where(x => x.ReleaseID == RelID).ToList();
                return products;
            }
        }

        public void AddToWishlist(int productID)
        {
            int accountID = AccountID.AI;

            MessageBox.Show($"{accountID.ToString()} and {productID.ToString()}");
            using (Data d = new Data())
            {
                Wishlist toAdd = new Wishlist(accountID, productID);
                d.wishlist.Add(toAdd);
                d.SaveChanges();
            }
        }
        public List<Product> GetWishlist(int accountID)
        {

            Data d = new Data();
            List<Product> wishlist = new List<Product>();
            List<int> productIDs = d.wishlist.Where(x => x.AccountID == accountID).Select(x => x.ProductID).ToList();
            foreach (int ID in productIDs)
            {
                wishlist.Add(d.product.FirstOrDefault(x => x.ProductID == ID));
            }
            return wishlist;

        }
        public void AddProductToCart()
        {

        }
        public void AddToLikeList(int releaseID)
        {
            int accountID = AccountID.AI;

            MessageBox.Show($"account {accountID.ToString()}, release {releaseID.ToString()}");

            using (Data d = new Data())
            {
                Likedlist toAdd = new Likedlist(accountID, releaseID);
                d.likedlist.Add(toAdd);
                d.SaveChanges();
            }
        }
        public List<release> GetLikedList(int accountID)
        {

            Data d = new Data();
            List<release> likedList = new List<release>();
            List<int> productIDs = d.likedlist.Where(x => x.AccountID == accountID).Select(x => x.ReleaseID).ToList();
            foreach (int ID in productIDs)
            {
                likedList.Add(d.release.FirstOrDefault(x => x.ReleaseID == ID));
            }
            return likedList;

        }
        public string[] PasswordCheck(string pass)
        {
            Regex DigitCheck = new Regex(@"\d");
            Regex LowercaseCheck = new Regex(@"[a-z]");
            Regex UppercaseCheck = new Regex(@"[A-Z]");
            Regex SpecialCheck = new Regex(@"\W");
            if (pass.Length < 8)
            {
                return ["Password must be at least 8 characters long", "Invalid Length"];
            }
            else if (!LowercaseCheck.IsMatch(pass))
            {
                return ["Password must contain at least 1 lowercase character", "Invalid case"];
            }
            else if (!UppercaseCheck.IsMatch(pass))
            {
                return ["Password must contain at least 1 uppercase character", "Invalid case"];
            }
            else if (!SpecialCheck.IsMatch(pass))
            {
                return ["Password must contain at least 1 special character", "No special characters detected"];
            }
            return ["Correct"];
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

            MySqlCommand DuplicateCheck = new MySqlCommand("SELECT genre, subgenre FROM musicspot.genre;", (MySqlConnection)connection);

            MySqlCommand commandDatabase = new MySqlCommand("INSERT INTO musicspot.genre (genre, subgenre) VALUES (@GenreID, @SubgenreID);", (MySqlConnection)connection);
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

