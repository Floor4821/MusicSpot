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
            { "Rock", 10 },
            { "Soul", 11 }
        };

        private Dictionary<string, int> SubgenreType = new Dictionary<string, int> {
            { "Ambient", 1},
            { "DarkAmbient", 2 },
            { "ElectronicAmbient", 3 },
            { "BitMusic", 4 },
            { "Breakbeat", 5 },
            { "BubblegumBass", 6 },
            { "Chillout" , 7 },
            { "DrumAndBass", 8 },
            { "Dubstep", 9 },
            { "ElectroIndustrial", 10 },
            { "Electronic", 11 },
            { "GlitchHop", 12 },
            { "Hardcore", 13 },
            { "House", 14 },
            { "IDM", 15 },
            { "Indietronica", 16 },
            { "Techno", 17 },
            { "TripHop", 18 },
            { "Drone", 19 },
            { "Experimental", 20 },
            { "Noise", 21 },
            { "Plunderphonics", 22 },
            { "AvantFolk", 23 },
            { "ChamberFolk", 24 },
            { "FolkRock", 25 },
            { "IndieFolk", 26 },
            { "AbstractHipHop", 27 },
            { "ConsciousHipHop", 28 },
            { "ExperimentalHipHop", 29 },
            { "HardcoreHipHop", 30 },
            { "JazzRap", 31 },
            { "PopRap", 32 },
            { "Trap", 33 },
            { "AvantGardeJazz", 34 },
            { "CoolJazz", 35 },
            { "HardBop", 36 },
            { "JazzFusion", 37 },
            { "ModalJazz", 38 },
            { "SmoothJazz", 39 },
            { "AlternativeMetal", 40 },
            { "AvantGardeMetal", 41 },
            { "BlackMetal", 42 },
            { "DeathMetal", 43 },
            { "HeavyMetal", 44 },
            { "ProgressiveMetal", 45 },
            { "StonerMetal", 46 },
            { "ThrashMetal", 47 },
            { "ArtPop", 48 },
            { "BaroquePop", 49 },
            { "DancePop", 50 },
            { "Electropop", 51 },
            { "GlitchPop", 52 },
            { "IndiePop", 53 },
            { "JanglePop", 54 },
            { "ProgressivePop", 55 },
            { "PsychedelicPop", 56 },
            { "Synthpop", 57 },
            { "ArtPunk", 58 },
            { "Emo", 59 },
            { "GothicRock", 60 },
            { "HardcorePunk", 61 },
            { "PopPunk", 62 },
            { "PostHardcore", 63 },
            { "PostPunk", 64 },
            { "PunkRock", 65 },
            { "AlternativeRock", 66 },
            { "ArtRock", 67 },
            { "DreamPop", 68 },
            { "ExperimentalRock", 69 },
            { "GarageRock", 70 },
            { "Grunge", 71 },
            { "HardRock", 72 },
            { "IndieRock", 73 },
            { "IndustrialRock", 74 },
            { "MathRock", 75 },
            { "NewWave", 76 },
            { "NoiseRock", 77 },
            { "PopRock", 78 },
            { "PostRock", 79 },
            { "ProgressiveRock", 80 },
            { "PsychedelicRock", 81 },
            { "Shoegaze", 82 },
            { "SlackerRock", 83 },
            { "Slowcore", 84 },
            { "NeoSoul", 85 },
            { "ProgressiveSoul", 86 },
            { "Soul", 87 }
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

        public void AddEnums()
        {
            
        }

        public int CreateAccount(Account account)
        {
            string query = $"INSERT INTO account(accountID, name, email, password, isadmin, profilepic) " +
                $"VALUES (NULL, '{account.AccountName}', '{account.Email}', '{account.Password}'," +
                $" '{account.IsAdmin}', '{account.ProfilePic}');";

            return this.Insert(query);
        }

    }
}
