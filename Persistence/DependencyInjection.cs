using AspNetCore.Lib.Configurations;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using AspNetCore.Lib.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.EFModels;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));
            
            var resultServices = TypeRegister.ScanAssemblyTypes(Assembly.GetExecutingAssembly())
                .ToList();

            resultServices.RegisterServicesByLifeTime(services);
            
            return services;
        }
    }
}
