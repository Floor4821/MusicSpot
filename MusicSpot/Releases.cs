using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class Release
    {
        public string ReleaseName { get; set; }
        public ReleaseType ReleaseType { get; set; }
        public string Cover { get; set; }
        public List<Songs> Songs { get; set; }
        public Artist Artist { get; set; }
        public List<Product> Products { get; set; }

        public Release(string releaseName, ReleaseType releaseType, string cover, List<Song> songs, Artist artist)
        {
            ReleaseName = releaseName;
            ReleaseType = releaseType;
            Cover = cover;
            Songs = songs;
            Artist = artist;
            Products = new List<Product>();
        }
    }


    public class GenreClass
    {
        public EnumSubGenre Subgenre { get; set; }
        public EnumGenre Genre { get; set; }

        public GenreClass(EnumSubGenre subgenre, EnumGenre genre)
        {
            Subgenre = subgenre;
            Genre = genre;
        }
    }

    public class Artist
    {
        public int ID { get; set; }
        public string ArtistName { get; set; }
        public List<Person> People { get; set; }

        private Data data = new Data();

        public Artist(string artistName, List<Person> people)
        {
            ArtistName = artistName;
            People = people;

            ID = data.InitiateArtist(this);
        }

        public override string ToString()
        {
            string output = $"Added artist {ID}: {ArtistName}\n";
            foreach (Person p in People)
            {
                output += $"- {p.PersonName} ({p.ID})\n";
            }

            return output;
        }
    }

    public class Person
    {
        public int ID { get; set; }
        public string PersonName { get; set; }
        public List<Artist> Artists { get; set; }

        private Data data = new Data();

        public Person(string personName)
        {
            PersonName = personName;
            Artists = new List<Artist>();

            ID = data.AddPerson(this);
        }
    }

    public class Song
    {
        public string SongName { get; set; }

        public Song(string songName, int songLength)
        {
            SongName = songName;
        }
    }

    public enum EnumSubGenre
    {
        SynthPop,
        IDM
    }

    public enum EnumGenre
    {
        Pop,
        HipHop,
        Rock
    }
    public enum ReleaseType
    {
        Album,
        EP,
        Single,
        Other
    }
}
