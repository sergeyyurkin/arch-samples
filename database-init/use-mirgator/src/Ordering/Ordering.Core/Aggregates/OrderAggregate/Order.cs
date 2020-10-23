using System.Collections.Generic;
using Ordering.Core.Aggregates.BuyerAggregate;

namespace Ordering.Core.Aggregates.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public int BuyerId { get; set; }
        public Buyer Buyer { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
