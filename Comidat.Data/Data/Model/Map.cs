using System.ComponentModel.DataAnnotations;

namespace Comidat.Data.Model
{
    public class Map
    {
        [Key]
        [Obfuscation(Exclude = true)]
        public int Id { set; get; }

        [Obfuscation(Exclude = true)]
        public string FileName { set; get; }
        [Obfuscation(Exclude = true)]
        public string Name { set; get; }
        [Obfuscation(Exclude = true)]
        public int Width { set; get; }
        [Obfuscation(Exclude = true)]
        public int Height { set; get; }
        [Obfuscation(Exclude = true)]
        public string Floor { set; get; }
        [Obfuscation(Exclude = true)]
        public int FloorDepth { set; get; }
        [Obfuscation(Exclude = true)]
        public bool Active { set; get; }
    }
}