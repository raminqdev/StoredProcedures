using System.Linq;
using System.Reflection;
using AspNetCore.Lib.Configurations;
using AspNetCore.Lib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class DependencyInjection
    {
         public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
        {
            
            var coreResultServices = new AspNetCore.Lib.Configurations.DependencyInjection()
                .AddAspNetCoreLayer(services);
            coreResultServices.RegisterServicesByLifeTime(services);
            
            
            var resultServices = TypeRegister.ScanAssemblyTypes(Assembly.GetExecutingAssembly())
               .ToList();
             resultServices.RegisterServicesByLifeTime(services);
             
            
            return services;
        }
    }
}