using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Comidat.Data.Model
{
    public class TBLTag
    {
        [Key] [Obfuscation(Exclude = true)] public long Id { get; set; }

        [Obfuscation(Exclude = true)] public string TagFirstName { get; set; }

        [Obfuscation(Exclude = true)] public string TagLastName { get; set; }

        [Obfuscation(Exclude = true)] public string TagFullName { get; set; }

        [Obfuscation(Exclude = true)] public string TagMacAddress { get; set; }

        [Obfuscation(Exclude = true)] public int TagNumber { get; set; }

        [Obfuscation(Exclude = true)] public bool TagActive { get; set; }

        [Obfuscation(Exclude = true)] public string TagStaticTelephone { get; set; }

        [Obfuscation(Exclude = true)] public string TagMobilTelephone { get; set; }

        [Obfuscation(Exclude = true)] public string TagDescription { get; set; }

        [Obfuscation(Exclude = true)] public string TagTCNo { get; set; }

        [Obfuscation(Exclude = true)] public int? RecordUserId { get; set; }

        [Obfuscation(Exclude = true)] public DateTime RecordDateTime { get; set; }

        [Obfuscation(Exclude = true)] public int? UpdateUserId { get; set; }

        [Obfuscation(Exclude = true)] public DateTime? UpdateDateTime { get; set; }

        [Obfuscation(Exclude = true)] public bool Deleted { get; set; }
    }
}