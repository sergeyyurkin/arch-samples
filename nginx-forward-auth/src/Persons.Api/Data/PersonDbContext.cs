using Microsoft.EntityFrameworkCore;
using Persons.Api.Data.Configurations;
using Persons.Api.Data.Entities;

namespace Persons.Api.Data
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersoneEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Persone> Persons { get; set; }
    }
}
