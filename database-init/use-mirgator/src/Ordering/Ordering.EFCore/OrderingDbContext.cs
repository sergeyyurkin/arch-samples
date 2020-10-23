using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Ordering.Core.Aggregates.BuyerAggregate;
using Ordering.Core.Aggregates.OrderAggregate;
using Ordering.EFCore.Configurations;

namespace Ordering.EFCore
{
    public class OrderingDbContext : DbContext
    {
        public OrderingDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
    }
}
