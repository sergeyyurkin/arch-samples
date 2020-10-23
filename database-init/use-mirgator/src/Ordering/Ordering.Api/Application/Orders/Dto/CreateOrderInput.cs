using System.Collections.Generic;

namespace Ordering.Api.Application.Orders.Dto
{
    public class CreateOrderInput
    {
        public int BuyerId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public IEnumerable<CreateOrderItemDto> Items { get; set; }
    }

    public class CreateOrderItemDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
