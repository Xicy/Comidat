using System;
using System.ComponentModel.DataAnnotations;

namespace Comidat.Data.Model
{
    public class Location
    {
        [Key]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int Id { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public virtual Map Map { set; get; }

        // ReSharper disable once InconsistentNaming
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public virtual ESP ESP { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double X { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double Y { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double Z { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public DateTime InsertDateTime { set; get; }

        // ReSharper disable InconsistentNaming
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double TX { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double TY { set; get; }
        // ReSharper restore InconsistentNaming
    }
}