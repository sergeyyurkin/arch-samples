using System;
using System.Linq;
using Ordering.Core.Aggregates.BuyerAggregate;

namespace Ordering.EFCore.Seed
{
    public class BuyerDbBuilder
    {
        private readonly OrderingDbContext _context;

        public BuyerDbBuilder(OrderingDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            if (!_context.Buyers.Any())
            {
                var buyers = new Buyer[]
                {
                        Buyer.Create(Guid.NewGuid().ToString(), "Alice"),
                        Buyer.Create(Guid.NewGuid().ToString(), "Bob"),
                };

                _context.AddRange(buyers);
                _context.SaveChanges();
            }
        }
    }
}
