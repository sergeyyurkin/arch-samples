namespace Ordering.Core.Aggregates.OrderAggregate
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }

        protected OrderItem()
        {
        }

        public static OrderItem Create(string name, decimal price, int units)
        {
            return new OrderItem
            {
                ProductName = name,
                UnitPrice = price,
                Units = units
            };
        }
    }
}
