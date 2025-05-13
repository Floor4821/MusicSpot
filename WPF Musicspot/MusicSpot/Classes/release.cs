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
using ImageMagick;
using MusicSpot.Classes;

namespace MusicSpot
{
    public class release
    {
        [Key]
        public int ReleaseID { get; set; }
        public string ReleaseName { get; set; }
        public byte[]? Cover { get; set; }
        public int Releasetype { get; set; }
        public string Artist { get; set; }
        public DateTime Releasedate { get; set; }
        public int GenreID { get; set; }
        
        [NotMapped]
        public ImageSource CoverImage
        {
            get
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "album.png");
                if (Cover != null)
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
                        return new BitmapImage(new Uri(path));
                    }
                }
                else
                {
                    return new BitmapImage(new Uri(path));
                }
            }
        }
        [NotMapped]
        public string GenreString
        {
            get
            {
                var data = new Data();
                GenreObject Releasegenre = data.genreobject.FirstOrDefault(x => x.GenreID == GenreID);
                Genretype GT = data.genretype.FirstOrDefault(w => w.ID == Releasegenre.Genre);
                return GT.Type;
            }
        }
        [NotMapped]
        public string SubgenreString
        {
            get
            {
                var data = new Data();
                GenreObject Releasegenre = data.genreobject.FirstOrDefault(x => x.GenreID == GenreID);
                SubgenreType SGT = data.subgenretype.FirstOrDefault(w => w.ID == Releasegenre.Subgenre);
                return SGT.Type;
            }
        }
        [NotMapped]
        public string ReleaseTypeString
        {
            get
            {
                var data = new Data();
                switch (Releasetype)
                {
                    case 1:
                        return "Album";
                    case 2:
                        return "EP";
                    default:
                        return "Media not defined";
                }
            }
        }
        public release()
        {
            
        }
        public release(string releasename,byte[] cover, int rt, string artist, DateTime releasedate, int genreID)
        {
            ReleaseName = releasename;
            Cover = cover;
            Releasetype = rt;
            Artist = artist;
            Releasedate = releasedate;
            GenreID = genreID;
        }
        public ImageSource ConvertWebPToImageSource(byte[] webPBytes)
        {
            using (var ms = new MemoryStream(webPBytes))
            {
                using (var image = new MagickImage(ms))
                {
                    using (var outStream = new MemoryStream())
                    {
                        image.Format = MagickFormat.Bmp; // Convert to a compatible format
                        image.Write(outStream);
                        outStream.Position = 0;

                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = outStream;
                        bitmap.EndInit();
                        return bitmap;
                    }
                }
            }
        }
    }
}
