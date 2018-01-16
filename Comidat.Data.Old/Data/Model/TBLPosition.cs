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
        [Obfuscation(Exclude = true)]
        public long Id { get; set; }

        [Obfuscation(Exclude = true)]
        public long TagId { get; set; }

        [Obfuscation(Exclude = true)]
        public int MapId { get; set; }

        [Obfuscation(Exclude = true)]
        public int XPosition { get; set; }

        [Obfuscation(Exclude = true)]
        public int YPosition { get; set; }

        [Obfuscation(Exclude = true)]
        public int ZPosition { get; set; }

        [Obfuscation(Exclude = true)]
        public int d_XPosition { get; set; }

        [Obfuscation(Exclude = true)]
        public int d_yPosition { get; set; }

        [Obfuscation(Exclude = true)]
        public DateTime RecordDateTime { get; set; }
    }
}