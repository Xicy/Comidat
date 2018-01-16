using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Comidat.Data.Model
{
    public class TBLReader
    {
        [Key]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int local_id { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string ReaderName { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int MapId { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string rd_mac_address { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int rd_floor { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double rd_pos_x { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double rd_pos_y { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double rd_pos_z { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double d_rd_pos_x { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double d_rd_pos_y { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public bool istreed { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public double currnetMetre { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string AssignIpAddress { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string EspMacId { get; set; }
    }
}