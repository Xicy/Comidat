﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Comidat.Data.Model
{
    public class TBLMap
    {
        [Key]
        [Obfuscation(Exclude = true)]
        public int Id { get; set; }

        [Obfuscation(Exclude = true)]
        public string MapName { get; set; }

        [Obfuscation(Exclude = true)]
        public string mp_image { get; set; }

        [Obfuscation(Exclude = true)]
        public float MapWidth { get; set; }

        [Obfuscation(Exclude = true)]
        public float MapHeight { get; set; }

        [Obfuscation(Exclude = true)]
        public int Floor { get; set; }

        [Obfuscation(Exclude = true)]
        public double FloorDepth { get; set; }

        [Obfuscation(Exclude = true)]
        public bool MapActive { get; set; }

        [Obfuscation(Exclude = true)]
        public int RecordUserId { get; set; }

        [Obfuscation(Exclude = true)]
        public DateTime RecordDateTime { get; set; }

        [Obfuscation(Exclude = true)]
        public int? UpdateUserId { get; set; }

        [Obfuscation(Exclude = true)]
        public DateTime? UpdateDateTime { get; set; }

        [Obfuscation(Exclude = true)]
        public bool Deleted { get; set; }
    }
}