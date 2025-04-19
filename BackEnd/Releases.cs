using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSpot
{
    public class Release
    {
        public int ReleaseID { get; set; }
        public string ReleaseName { get; set; }
        public string Artist { get; set; }
        public byte[] Cover { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ReleaseType ReleaseType { get; set; } 
        public List<string> Songs { get; set; }
        public Genre Genre { get; set; }
        public List<Product> Products { get; set; }

        public Release(string releaseName, string artist, byte[] cover, DateTime releaseDate, ReleaseType releaseType, List<string> songs, Genre genre, List<Product> products)
        {
            ReleaseName = releaseName;
            Artist = artist;
            Cover = cover;
            ReleaseDate = releaseDate;
            Songs = songs;
            Genre = genre;
            Products = products;
        }
    }


    public class Genre
    {
        public int GenreID { get; set; }
        public EnumSubGenre Subgenre { get; set; }
        public EnumGenre GenreType { get; set; }

        public Genre(EnumSubGenre subgenre, EnumGenre genretype)
        {
            Subgenre = subgenre;
            GenreType = genretype;
        }
    }

    public enum EnumSubGenre
    {
        Ambient,
        DarkAmbient,
        ElectronicAmbient,
        BitMusic,
        Breakbeat,
        BubblegumBass,
        Chillout,
        DrumAndBass,
        Dubstep,
        ElectroIndustrial,
        Electronic,
        GlitchHop,
        Hardcore,
        House,
        IDM,
        Indietronica,
        Techno,
        TripHop,
        Drone,
        Experimental,
        Noise,
        Plunderphonics,
        AvantFolk,
        ChamberFolk,
        FolkRock,
        IndieFolk,
        AbstractHipHop,
        ConsciousHipHop,
        ExperimentalHipHop,
        HardcoreHipHop,
        JazzRap,
        PopRap,
        Trap,
        AvantGardeJazz,
        CoolJazz,
        HardBop,
        JazzFusion,
        ModalJazz,
        SmoothJazz,
        AlternativeMetal,
        AvantGardeMetal,
        BlackMetal,
        DeathMetal,
        HeavyMetal,
        ProgressiveMetal,
        StonerMetal,
        ThrashMetal,
        ArtPop,
        BaroquePop,
        DancePop,
        Electropop,
        GlitchPop,
        IndiePop,
        JanglePop,
        ProgressivePop,
        PsychedelicPop,
        Synthpop,
        ArtPunk,
        Emo,
        GothicRock,
        HardcorePunk,
        PopPunk,
        PostHardcore,
        PostPunk,
        PunkRock,
        AlternativeRock,
        ArtRock,
        DreamPop,
        ExperimentalRock,
        GarageRock,
        Grunge,
        HardRock,
        IndieRock,
        IndustrialRock,
        MathRock,
        NewWave,
        NoiseRock,
        PopRock,
        PostRock,
        ProgressiveRock,
        PsychedelicRock,
        Shoegaze,
        SlackerRock,
        Slowcore,
        NeoSoul,
        ProgressiveSoul,
        Soul
    }

    public enum EnumGenre
    {
        Ambient,
        Electronic,
        Experimental,
        Folk,
        HipHop,
        Jazz,
        Metal,
        Pop,
        Punk,
        Rock,
        Soul
    }
    public enum ReleaseType
    {
        Album,
        EP,
        Single,
        Other
    }
}
