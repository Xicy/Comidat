using System.Reflection;
using Comidat.Data.Model;
#if EF6
using System.Data.Entity;
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
#else
using Microsoft.EntityFrameworkCore;
#endif

namespace Comidat.Data
{
    public sealed class DatabaseContext : DbContext
    {
        private readonly string _database;
        private readonly string _password;
        private readonly string _server;
        private readonly string _user;

#if DEBUG
        public DatabaseContext() : this("sql.lc", "ComidatOld", "SA", "Umut1996") 
#else
        public DatabaseContext() : this(".", "MinePts", "sa", "comidat")
#endif
        { }

        private DatabaseContext(string server, string database, string user, string password)
        {
            _server = server;
            _database = database;
            _user = user;
            _password = password;

#if EF6
            Database.Connection.ConnectionString =
                $@"Server={_server};Database={_database};User ID={_user};Password={
                        _password
                    };MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
            Database.CreateIfNotExists();
#else
            //Database.EnsureDeleted();
            Database.EnsureCreated();
#endif
        }

        [Obfuscation(Exclude = true)] public DbSet<TBLMap> TBLMaps { get; set; }

        [Obfuscation(Exclude = true)] public DbSet<TBLPosition> TBLPositions { get; set; }

        [Obfuscation(Exclude = true)] public DbSet<TBLReader> TBLReaders { get; set; }

        [Obfuscation(Exclude = true)] public DbSet<TBLTag> TBLTags { get; set; }
#if !EF6
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                $@"Server={_server};Database={_database};User ID={_user};Password={_password};MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");
        }
#else
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Comidat");
            base.OnModelCreating(modelBuilder);
        }
#endif

    }
}