using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MusicSpot
{
    public class Data
    {
        private string connectionString = "datasource=127.0.0.1; port=3307; username=root; password=; database=musicspot;";

        private Dictionary<string, int> MediaType = new Dictionary<string,int>{
            { "Vinyl", 1 }, 
            { "CD", 2 }, 
            { "Casette", 3 } 
        };
        private Dictionary<string, int> ReleaseType = new Dictionary<string, int>{
            { "Album", 1 },
            { "EP", 2 },
            { "Single", 3 },
            { "Other", 4 }
        };
        private Dictionary<string, int> GenreType = new Dictionary<string, int> {
            { "Ambient", 1 },
            { "Electronic", 2 },
            { "Experimental", 3 },
            { "Folk", 4 },
            { "HipHop", 5 },
            { "Jazz", 6 },
            { "Metal", 7 },
            { "Pop", 8 },
            { "Punk", 9 },
            { "Rock", 10 }
        };
        private int Insert(string query)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                int result = commandDatabase.ExecuteNonQuery();
                return (int)commandDatabase.LastInsertedId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public int CreateAccount(Account account)
        {
            string query = $"INSERT INTO account(accountID, name, email, password, isadmin, profilepic) " +
                $"VALUES (NULL, '{account.AccountName}', '{account.Email}', '{account.Password}'," +
                $" '{account.IsAdmin}', '{account.ProfilePic}');";

            return this.Insert(query);
        }

        public int InitiateArtist(Artist artist)
        {

            int artistID = AddArtist(artist);

            string query = $"INSERT INTO artistperson(artistpersonID, artistID, personID) " +
                $"VALUES ";


            foreach (Person p in artist.People)
            {
                int personID = p.ID;
                query += $"(NULL, {artistID}, {personID}), ";
            }
            query = query.TrimEnd(',', ' ');
            return this.Insert(query);
        }

        public int AddArtist(Artist artist)
        {
            string query = $"INSERT INTO artist(artistID, name) " +
                $"VALUES (NULL, '{artist.ArtistName}'); ";

            return this.Insert(query);
        }

        public int AddPerson(Person person)
        {
            string query = $"INSERT INTO person(personID, name) " +
                $"VALUES (NULL, '{person.PersonName}');";

            return this.Insert(query);
        }

            
    }
}
