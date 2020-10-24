namespace Ordering.EFCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedDb(OrderingDbContext context)
        {
            new BuyerDbBuilder(context).Create();
            // etc
        }
    }
}
