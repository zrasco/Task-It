using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TaskItSite.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaskItSite.Models;

namespace TaskItSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSecrets.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var host = BuildWebHost(args, configuration);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // zrasco - This code execites if the database hasn't been created yet
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (DbUpdateException ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An SQL error occurred while seeding the database.");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "A general error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args, IConfiguration config) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseKestrel(options =>
                {
                    options.Limits.MinResponseDataRate = null;
                })
                .UseConfiguration(config)
                .Build();
    }
}
