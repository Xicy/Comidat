using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Comidat.Data.Model
{
    public class TBLPosition
    {
        public TBLPosition()
        {
            RecordDateTime = DateTime.Now;
        }

        [Key]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public long Id { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public long TagId { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int MapId { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int XPosition { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int YPosition { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int ZPosition { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int d_XPosition { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int d_yPosition { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public DateTime RecordDateTime { get; set; }
    }
}