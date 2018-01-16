using System;
using System.ComponentModel.DataAnnotations;

namespace Comidat.Data.Model
{
    public class Admin
    {
        [Key]
        [Obfuscation(Exclude = false, Feature = "-rename")]
        public int Id { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string UserName { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string Email { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public string Password { set; get; }

        [Obfuscation(Exclude = false, Feature = "-rename")]
        public DateTime LastLoginDateTime { set; get; }
    }
}