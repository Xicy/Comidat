using System.ComponentModel.DataAnnotations;

namespace Comidat.Data.Model
{
    public class Reader
    {
        [Key]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int Id { set; get; }


        // ReSharper disable once InconsistentNaming
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public virtual ESP ESP { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string Name { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string ApIp { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double X { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double Y { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double Z { set; get; }

        // ReSharper disable InconsistentNaming
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double TX { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double TY { set; get; }
        // ReSharper restore InconsistentNaming
    }
}