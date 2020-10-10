using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.Api.Extensions
{
    public static class IWebHostExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost host, Action<TContext> seedAction = null) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TContext>();
                InvokeSeeder(seedAction, context);
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext> seedAction, TContext dbContext)
            where TContext : DbContext
        {
            dbContext.Database.Migrate();
            seedAction?.Invoke(dbContext);
            dbContext.SaveChanges();
        }
    }
}
