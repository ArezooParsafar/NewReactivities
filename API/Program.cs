using Application.Logger;
using Domain.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using Persistence.DatabaseConfig;
using System;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                .ConfigureLogging((hostingContext, logging)=>
                {
                    logging.ClearProviders();
                    logging.ClearProviders();

                    logging.AddDebug();

                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddConsole();
                    }

                    logging.AddDbLogger(); // You can change its Log Level using the `appsettings.json` file -> Logging -> LogLevel -> Default
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

                })
               .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try

                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    context.Database.Migrate();
                    Seed.SeedData(context, userManager).Wait();

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();

         }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                         WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

    }
}
