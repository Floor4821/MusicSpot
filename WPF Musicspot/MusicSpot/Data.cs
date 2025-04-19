using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient; // Import MySql.Data only
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Policy;
using System.Linq.Expressions;

namespace MusicSpot
{
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
                // Set lowercase table name
                entity.SetTableName(entity.GetTableName().ToLower());

                // Set lowercase column names
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            }
        }
        /*INSERT INTO musicspot.release (cover, releasetype, artist, releasedate, releasename, genreID) VALUES
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg...', 1, 'Aurora Lane', '2023-07-14 00:00:00', 'Starlight Echoes', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg', 2, 'Neon Heights', '2022-11-08 00:00:00', 'City Lights', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg', 3, 'EchoNova', '2024-01-21 00:00:00', 'Orbit', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg=', 1, 'Velvet Horizon', '2021-05-30 00:00:00', 'Waves', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg=', 2, 'Crimson Bloom', '2023-03-18 00:00:00', 'Petals & Flames', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg=', 3, 'Skybound', '2020-09-10 00:00:00', 'Above the Clouds', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg', 1, 'Lunar Bloom', '2024-12-25 00:00:00', 'Silent Nights', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg', 2, 'Violet Noir', '2022-02-14 00:00:00', 'Dark Love', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg=', 3, 'Atlas Dream', '2023-08-04 00:00:00', 'Horizons', 1),
('C:\Users\jamey\Vakken Semester 2\Inspiration Lab\Song.jpg=', 1, 'Nova & Co.', '2021-12-31 00:00:00', 'The Last Light', 1);
        */

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
    }
}

