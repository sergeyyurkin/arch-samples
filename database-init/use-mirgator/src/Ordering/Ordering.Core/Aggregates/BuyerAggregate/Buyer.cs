namespace Ordering.Core.Aggregates.BuyerAggregate
{
    public class Buyer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }


        protected Buyer()
        {
        }

        public static Buyer Create(string userId, string name)
        {
            return new Buyer { UserId = userId, Name = name };
        }
    }
}
