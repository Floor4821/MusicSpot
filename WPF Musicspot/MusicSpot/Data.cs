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

namespace MusicSpot
{
    // SQL ENTITYFRAMEWORK CORE
    //_________________________________________________________________________________
    public class Data: DbContext
    {
        public DbSet<release> release { get; set; }
        public DbSet<UserAccount> account { get; set; }
        
        private string connectionstring = "datasource = 127.0.0.1;" +
            "port = 3307;" +
            "username = root;" +
            "password = ;" +
            "database = musicspot;";

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
        public void InsertGenre()
        {
            var connection = this.Database.GetDbConnection();

            MySqlCommand commandDatabase = new MySqlCommand("INSERT INTO musicspot.genre (genre, subgenre) VALUES (2, 2);", (MySqlConnection)connection);
            try
            {
                connection.Open();
                commandDatabase.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                    $"AS GT INNER JOIN musicspot.release AS MR " +
                    $"ON GT.ID = MR.genreID WHERE '{r.ReleaseID}' = MR.releaseID;", (MySqlConnection)connection);
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

