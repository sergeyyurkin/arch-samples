using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Persons.Api.Data;
using Persons.Api.Extensions;

namespace Persons.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                host.MigrateDbContext<PersonDbContext>();
                host.Run();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
