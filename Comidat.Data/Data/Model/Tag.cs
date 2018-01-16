using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comidat.Data.Model
{
    public class Tag
    {
        public Tag()
        {
            BirthDate = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public long TcNumber { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string FirstName { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string LastName { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string Phone { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string MobilePhone { set; get; }
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public DateTime BirthDate { set; get; }

        // ReSharper disable once InconsistentNaming
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public virtual ESP ESP { set; get; }
    }
}