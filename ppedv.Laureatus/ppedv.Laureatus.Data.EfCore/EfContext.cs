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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Person>().Property(x => x.Job).HasMaxLength(72).HasColumnName("Beruf");

            modelBuilder.Entity<Price>().HasIndex(x => x.Year).IsUnique();

            //modelBuilder.Entity<Person>().HasMany(x => x.Laureates).WithOne(x => x.Person).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Laureate>().HasOne(x => x.Person).WithMany(x => x.Laureates).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Laureate>().HasOne(x => x.Price).WithMany(x => x.Laureates).IsRequired().OnDelete(DeleteBehavior.Cascade);

        }

        public override int SaveChanges()
        {
            var now = DateTime.Now;
            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                if (item.Entity is Entity en)
                {
                    en.Created = now;
                    en.Modified = now;
                }
            }

            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
            {
                if (item.Entity is Entity en)
                    en.Modified = now;
            }

            return base.SaveChanges();
        }
    }
}