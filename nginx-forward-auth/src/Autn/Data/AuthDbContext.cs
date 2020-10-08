using Auth.Data.Configurations;
using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
     
        public DbSet<ApplicatinUser> Users { get; set; }
    }
}
