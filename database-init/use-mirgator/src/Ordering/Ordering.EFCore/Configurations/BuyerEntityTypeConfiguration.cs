using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Core.Aggregates.BuyerAggregate;

namespace Ordering.EFCore.Configurations
{
    public class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder
                .ToTable("Buyers");

            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder
                .Property(b => b.Name)
                .HasColumnName("Name")
                .IsRequired();
        }
    }
}
