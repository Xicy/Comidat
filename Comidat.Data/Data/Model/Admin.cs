using System;
using System.ComponentModel.DataAnnotations;

namespace Comidat.Data.Model
{
    public class Admin
    {
        [Key]
        [Obfuscation(Exclude = true)]
        public int Id { set; get; }

        [Obfuscation(Exclude = true)]
        public string UserName { set; get; }

        [Obfuscation(Exclude = true)]
        public string Email { set; get; }

        [Obfuscation(Exclude = true)]
        public string Password { set; get; }

        [Obfuscation(Exclude = true)]
        public DateTime LastLoginDateTime { set; get; }
    }
}