
namespace Comidat.Data.Model
{
    public class TBLReader
    {
        public int local_id { get; set; }

        public string ReaderName { get; set; }

        public int MapId { get; set; }

        public string rd_mac_address { get; set; }

        public int rd_floor { get; set; }

        public double rd_pos_x { get; set; }

        public double rd_pos_y { get; set; }

        public double rd_pos_z { get; set; }

        public double d_rd_pos_x { get; set; }

        public double d_rd_pos_y { get; set; }

        public bool istreed { get; set; }

        public double currnetMetre { get; set; }

        public string AssignIpAddress { get; set; }

        public string EspMacId { get; set; }
    }
}