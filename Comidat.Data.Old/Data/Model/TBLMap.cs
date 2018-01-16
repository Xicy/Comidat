using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Comidat.Data.Model
{
    public class TBLMap
    {
        [Key]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int Id { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string MapName { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string mp_image { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public float MapWidth { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public float MapHeight { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int Floor { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double FloorDepth { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public bool MapActive { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int RecordUserId { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public DateTime RecordDateTime { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int? UpdateUserId { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public DateTime? UpdateDateTime { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public bool Deleted { get; set; }
    }
}