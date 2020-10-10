using Identity.Api.Data.Configurations;
using Identity.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Data
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new IdentityUserConfiguration());
        }
     
        public DbSet<IdentityUser> Users { get; set; }
    }
}
