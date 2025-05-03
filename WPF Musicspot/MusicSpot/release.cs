using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MusicSpot
{
    public class release
    {
        [Key]
        public int ReleaseID { get; set; }
        public string ReleaseName { get; set; }
        public byte[] Cover { get; set; }
        public releasetype Releasetype { get; set; }
        public string Artist { get; set; }
        public DateTime Releasedate { get; set; }
        public int GenreID { get; set; }
        
        [NotMapped]
        public ImageSource CoverImage
        {
            get
            {
                if(Cover != null)
                {
                    try
                    {
                        using (var ms = new MemoryStream(Cover))
                        {
                            var image = new BitmapImage();
                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.StreamSource = ms;
                            image.EndInit();
                            return image;
                        }
                    }
                    catch
                    {
                        return new BitmapImage(new Uri("https://i.postimg.cc/T3Yq5RgQ/Album.jpg"));
                    }
                }
                else
                {
                    return new BitmapImage(new Uri("https://i.postimg.cc/T3Yq5RgQ/Album.jpg"));
                }
            }
        }
        public release()
        {
            
        }
        public release(int releaseid, string releasename,byte[] cover, releasetype rt, string artist, DateTime releasedate, int genreID)
        {
            ReleaseID = releaseid;
            ReleaseName = releasename;
            Cover = cover;
            Releasetype = rt;
            Artist = artist;
            Releasedate = releasedate;
            GenreID = genreID;
        }
    }
    public enum releasetype
    {
        Album,
        EP,
        Single,
        Other
    }
}
