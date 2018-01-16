using System;
using System.ComponentModel.DataAnnotations;

namespace Comidat.Data.Model
{
    public class Location
    {
        [Key]
        [Obfuscation(Exclude = true)]
        public int Id { set; get; }

        [Obfuscation(Exclude = true)]
        public virtual Map Map { set; get; }

        // ReSharper disable once InconsistentNaming
        [Obfuscation(Exclude = true)]
        public virtual ESP ESP { set; get; }

        [Obfuscation(Exclude = true)]
        public double X { set; get; }
        [Obfuscation(Exclude = true)]
        public double Y { set; get; }
        [Obfuscation(Exclude = true)]
        public double Z { set; get; }
        [Obfuscation(Exclude = true)]
        public DateTime InsertDateTime { set; get; }

        // ReSharper disable InconsistentNaming
        [Obfuscation(Exclude = true)]
        public double TX { set; get; }

        [Obfuscation(Exclude = true)]
        public double TY { set; get; }
        // ReSharper restore InconsistentNaming
    }
}