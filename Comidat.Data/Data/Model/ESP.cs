﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comidat.Data.Model
{
    // ReSharper disable once InconsistentNaming
    public class ESP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("MacId")]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public long Id { private set; get; }

        [NotMapped]
        public ulong MacId
        {
            set
            {
                unchecked
                {
                    Id = (long)value;
                }
            }
            get
            {
                unchecked
                {
                    return (ulong)Id;
                }
            }
        }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string Ip { set; get; }

        // ReSharper disable once InconsistentNaming
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string SSID { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public bool Active { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public byte Batary { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string Description { set; get; }
    }
}