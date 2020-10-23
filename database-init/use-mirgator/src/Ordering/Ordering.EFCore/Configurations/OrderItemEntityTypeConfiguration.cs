using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Core.Aggregates.OrderAggregate;

namespace Ordering.EFCore.Configurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .ToTable("OrderItems");

            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.OrderId)
                .HasColumnName("OrderId")
                .IsRequired();

            builder
                .Property(o => o.UnitPrice)
                .HasColumnName("UnitPrice")
                .IsRequired();

            builder
                .Property(o => o.Units)
                .HasColumnName("Units")
                .IsRequired();

            builder
                .HasOne(o => o.Order)
                .WithMany(o => o.Items);
        }
    }
}
