using Comidat.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Comidat.Data
{
    public sealed class DatabaseContext : DbContext
    {
        private readonly string _server;
        private readonly string _database;
        private readonly string _user;
        private readonly string _password;

#if DEBUG
        public DatabaseContext() : this("sql.lc", "ComidatOld", "SA", "Umut1996") { }
#endif

        public DatabaseContext(string server, string database, string user, string password)
        {
            _server = server;
            _database = database;
            _user = user;
            _password = password;

            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        // ReSharper disable once InconsistentNaming
        [Obfuscation(Exclude = true)]
        public DbSet<ESP> ESPs { get; set; }
        [Obfuscation(Exclude = true)]
        public DbSet<Tag> TBLTags { get; set; }
        [Obfuscation(Exclude = true)]
        public DbSet<Reader> TBLReaders { get; set; }
        [Obfuscation(Exclude = true)]
        public DbSet<Location> Locations { get; set; }
        [Obfuscation(Exclude = true)]
        public DbSet<Map> TBLMaps { get; set; }
        [Obfuscation(Exclude = true)]
        public DbSet<Admin> Admins { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Server={_server};Database={_database};User ID={_user};Password={_password};");
        }
        
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Comidat");
            base.OnModelCreating(modelBuilder);
        }
        */
    }
}