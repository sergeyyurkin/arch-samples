using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Core.Aggregates.OrderAggregate;

namespace Ordering.EFCore.Configurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .ToTable("Orders");

            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.Address)
                .HasColumnName("Address")
                .IsRequired();

            builder
                .Property(o => o.Status)
                .HasColumnName("Status")
                .IsRequired();

            builder
                .Property(o => o.BuyerId)
                .HasColumnName("BuyerId")
                .IsRequired();
        }
    }
}
