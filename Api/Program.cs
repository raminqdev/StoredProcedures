using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using AspNetCore.Lib.Configurations;
using AspNetCore.Lib.Services.Interfaces;
using Microsoft.AspNetCore;
using Persistence.EFModels;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Statistics.WebHost = CreateWebHostBuilder(args).Build();
            
            using var scope = Statistics.WebHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger>();
                logger.Exception(ex, "An error occurred during migration");
            }
            
            await Statistics.WebHost.RunAsync();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
