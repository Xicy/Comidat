using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Comidat.Data.Model
{
    public class TBLTag
    {
        [Key]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public long Id { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string TagFirstName { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string TagLastName { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string TagFullName { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string TagMacAddress { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int TagNumber { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public bool TagActive { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string TagStaticTelephone { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string TagMobilTelephone { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string TagDescription { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string TagTCNo { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int? RecordUserId { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public DateTime RecordDateTime { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int? UpdateUserId { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public DateTime? UpdateDateTime { get; set; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public bool Deleted { get; set; }
    }
}