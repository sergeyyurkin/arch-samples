using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //var host = CreateHostBuilder(args).Build();
            //host.MigrateDbContext<AuthDbContext>((context) =>
                //new AuthDbContextSeed()
                //.SeedAsync(context)
                //.Wait());
            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
