using Identity.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Data.Configurations
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.ToTable("users");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).IsRequired();
            builder.Property(m => m.Login).HasColumnName("login").IsRequired();
            builder.Property(m => m.Password).HasColumnName("password").IsRequired();
            builder.Property(m => m.Email).HasColumnName("email").IsRequired();
        }
    }
}
