using Microsoft.EntityFrameworkCore;
using ppedv.Laureatus.Model;

namespace ppedv.Laureatus.Data.EfCore
{
    public class EfContext : DbContext
    {
        private readonly string conString;

        public DbSet<Person>? Persons { get; set; }
        public DbSet<Price>? Prices { get; set; }
        public DbSet<Laureate>? Laureates { get; set; }
        public DbSet<Category>? Categories { get; set; }

        public EfContext(string conString = "Server=(localdb)\\mssqllocaldb;Database=Laureatus;Trusted_Connection=true")
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
        }
    }
}