using System;

namespace Comidat.Data.Model
{
    public class TBLPosition
    {
        public TBLPosition()
        {
            RecordDateTime = DateTime.Now;
        }

        public long Id { get; set; }

        public long TagId { get; set; }

        public int MapId { get; set; }

        public int XPosition { get; set; }

        public int YPosition { get; set; }

        public int ZPosition { get; set; }

        public int d_XPosition { get; set; }

        public int d_yPosition { get; set; }

        public DateTime RecordDateTime { get; set; }
    }
}