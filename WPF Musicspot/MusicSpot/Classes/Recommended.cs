using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot.Classes
{
    public class Recommended
    {
        public (List<string>, List<string>) CreateRecScheme(int accountID)
        {
            Dictionary<string, int> subgenreCount = new Dictionary<string, int>();
            Dictionary<string, int> genreCount = new Dictionary<string, int>();
            List<release> likedList = new List<release>();
            List<int> likedListIDs = new List<int>();

            using (Data d = new Data())
            {
                likedList = d.GetLikedList(accountID);             
            }

            foreach (release r in likedList)
            {
                string subgenre = r.SubgenreString;
                string genre = r.GenreString;

                if (!subgenreCount.ContainsKey(subgenre))
                {
                    subgenreCount.Add(subgenre, 1);
                }
                else
                {
                    subgenreCount[subgenre]++;
                }

                if (!genreCount.ContainsKey(genre))
                {
                    genreCount.Add(genre, 1);
                }
                else
                {
                    genreCount[genre]++;
                }
            }

            List<string> sortedSubgenre = subgenreCount
                .OrderBy(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .ToList();

            List<string> sortedGenre = genreCount
                .OrderBy(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .ToList();

            return (sortedSubgenre, sortedGenre);
        }

        public List<release> SendRecommended(int accountID)
        {
            Random random = new Random();
            Data d = new Data();

            var lists = CreateRecScheme(accountID);
            List<string> subgenres = lists.Item1;
            List<string> genres = lists.Item2;

            Dictionary<string, List<release>> releasePool = GetReleasePool(subgenres, genres);
            List<release> recommended = new List<release>();
            List<release> likedList = d.GetLikedList(accountID);


            int subgenresLen = subgenres.Count;
            int genresLen = genres.Count;
            int total = subgenresLen + genresLen;
            for (int i = 0; recommended.Count <= 100; i++)
            {
                int depth = Math.Min(i, total);

                for (int j = 0; j < depth; j++)
                {
                    // select which genre to pick a release from
                    string search = "";
                    if (j >= subgenresLen)
                    {
                        search = genres[j - subgenresLen];
                    }
                    else
                    {
                        search = subgenres[j];
                    }
                    List<release> subList = releasePool[search];
                    
                    // pick random release from releases with that genre
                    if (subList.Count > 0)
                    {
                        int index = random.Next(subList.Count);
                        if (!(recommended.Contains(subList[index]) || likedList.Contains(subList[index])))
                        {
                            recommended.Add(subList[index]);
                        }
                        releasePool[search].RemoveAt(index);
                    }
                }
            }

            return recommended;
        }

        public Dictionary<string, List<release>> GetReleasePool(List<string> subgenres, List<string> genres)
        {
            using (Data d = new Data())
            {
                Dictionary<string, List<release>> releasePool = new Dictionary<string, List<release>>();
                foreach (string s in subgenres)
                {
                    int subgenreID = d.subgenretype.Where(x => x.Type == s).Select(x => x.ID).FirstOrDefault();

                    List<release> r = d.release.Where(x => x.GenreID == subgenreID).ToList();
                    releasePool[s] = r;
                }
                foreach (string s in genres)
                {
                    int genreID = d.genretype.Where(x => x.Type == s).Select(x => x.ID).FirstOrDefault();
                    List<int> subgenreIDs = d.genreobject.Where(x => x.Genre == genreID).Select(x => x.GenreID).ToList();
                    List<release> r = new List<release>();
                    foreach (int id in subgenreIDs)
                    {
                        r.AddRange(d.release.Where(x => x.GenreID == id).ToList());
                    }

                    releasePool[s] = r;
                    
                }
                return releasePool;
            }

        }
    }
}
