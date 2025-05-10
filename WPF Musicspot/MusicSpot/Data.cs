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

namespace MusicSpot
{
    // SQL ENTITYFRAMEWORK CORE
    //_________________________________________________________________________________
    public class Data: DbContext
    {
        public DbSet<release> release { get; set; }
        public DbSet<UserAccount> account { get; set; }
        public DbSet<Song> song { get; set; }
        public DbSet<Genretype> genretype { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<SubgenreType> subgenretype { get; set; }
        public DbSet<Genre> genre { get; set; }

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
                var ID = data.release.FirstOrDefault(x => x.ReleaseName == releasename);
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this release?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    data.release.Remove(ID);
                    data.SaveChanges();
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
        public void AddProductToCart()
        {

        }
        public void AddToLikeList(int accountid, int releaseid)
        {

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

