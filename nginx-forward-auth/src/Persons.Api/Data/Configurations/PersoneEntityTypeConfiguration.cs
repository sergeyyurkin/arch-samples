using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persons.Api.Data.Entities;

namespace Persons.Api.Data.Configurations
{
    public class PersoneEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persons");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.FirstName).HasColumnName("first_name");
            builder.Property(p => p.LastName).HasColumnName("last_name");
            builder.Property(p => p.BirthDate).HasColumnName("birth_date");
        }
    }
}
