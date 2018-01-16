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
        [Obfuscation(Exclude = true)]
        public long TcNumber { set; get; }

        [Obfuscation(Exclude = true)]
        public string FirstName { set; get; }
        [Obfuscation(Exclude = true)]
        public string LastName { set; get; }
        [Obfuscation(Exclude = true)]
        public string Phone { set; get; }
        [Obfuscation(Exclude = true)]
        public string MobilePhone { set; get; }
        [Obfuscation(Exclude = true)]
        public DateTime BirthDate { set; get; }

        // ReSharper disable once InconsistentNaming
        [Obfuscation(Exclude = true)]
        public virtual ESP ESP { set; get; }
    }
}