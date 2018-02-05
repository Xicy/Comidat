using System;

namespace Comidat.Data.Model
{
    public class TBLMap
    {
        public int Id { get; set; }

        public string MapName { get; set; }

        public string mp_image { get; set; }

        public double MapWidth { get; set; }

        public double MapHeight { get; set; }

        public int Floor { get; set; }

        public Single FloorDepth { get; set; }

        public bool MapActive { get; set; }

        public int RecordUserId { get; set; }

        public DateTime RecordDateTime { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public bool Deleted { get; set; }
    }
}