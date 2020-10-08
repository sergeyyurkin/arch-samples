using Auth.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration["CONNECTION_STRING"];
            services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(connectionString));

            services.AddAuthentication("Cookies").AddCookie();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                    await context.Response.WriteAsync(context.Request.Host.Value)
                );

                endpoints.MapGet("/health", async context =>
                    await context.Response.WriteAsync("{\"status\": \"OK\"}")
                );
            });
        }
    }
}
