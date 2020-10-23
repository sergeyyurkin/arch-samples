using System.Collections.Generic;
using Ordering.Core.Aggregates.BuyerAggregate;

namespace Ordering.Core.Aggregates.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }

        public string Address { get; private set; }
        public string Status { get; private set; }
        public string Description { get; private set; }

        public int BuyerId { get; private set; }
        public Buyer Buyer { get; private set; }


        public ICollection<OrderItem> Items { get; set; }


        protected Order()
        {
        }

        public static Order Create(string address, string description, int buyerId)
        {
            return new Order
            {
                Address = address,
                Status = "Created",
                Description = description,
                BuyerId = buyerId
            };
        }
    }
}
