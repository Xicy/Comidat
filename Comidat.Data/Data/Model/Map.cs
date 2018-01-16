using System.ComponentModel.DataAnnotations;

namespace Comidat.Data.Model
{
    public class Map
    {
        [Key]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int Id { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string FileName { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string Name { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int Width { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int Height { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string Floor { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int FloorDepth { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public bool Active { set; get; }
    }
}