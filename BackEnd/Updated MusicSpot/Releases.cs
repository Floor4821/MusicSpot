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
        [Key]
        public int releaseID { get; set; }
        [Required]
        public string ReleaseName { get; set; }
        [Required]
        public ReleaseType ReleaseType { get; set; } //*!
        public string Cover { get; set; }
        [Required]
        public List<string> Songs { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public List<Product> Products { get; set; }

        public Release(string releaseName, ReleaseType releaseType, string cover, List<string> songs, string artist)
        {
            ReleaseName = releaseName;
            ReleaseType = releaseType;
            Cover = cover;
            Songs = songs;
            Artist = artist;
            Products = new List<Product>();
        }
    }


    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        [Required]
        public EnumSubGenre Subgenre { get; set; }
        [Required]
        public EnumGenre GenreType { get; set; }

        public Genre(EnumSubGenre subgenre, EnumGenre genretype)
        {
            Subgenre = subgenre;
            GenreType = genretype;
        }
    }

    public class Song
    {
        [Key]
        public int SongID { get; set; }
        [Required]
        public string SongName { get; set; }
        public int SongLength { get; set; }
        public Song(string songName, int songlength)
        {
            SongName = songName;
            SongLength = songlength;
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
