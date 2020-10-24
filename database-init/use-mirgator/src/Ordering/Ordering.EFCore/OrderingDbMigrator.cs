using System;
using Microsoft.EntityFrameworkCore;

namespace Ordering.EFCore
{
    public class OrderingDbMigrator
    {
        private readonly OrderingDbContext _dbContext;

        public OrderingDbMigrator(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOrMigrate()
        {
            CreateOrMigrate(null);
        }

        public void CreateOrMigrate(Action<OrderingDbContext> seedAction)
        {
            _dbContext.Database.Migrate();
            seedAction?.Invoke(_dbContext);
            _dbContext.SaveChanges();
        }
    }
}
