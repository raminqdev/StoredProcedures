using AspNetCore.Lib.Configurations;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using AspNetCore.Lib.Extensions;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            var resultServices = TypeRegister.ScanAssemblyTypes(Assembly.GetExecutingAssembly())
                .ToList();

            resultServices.RegisterServicesByLifeTime(services);

            return services;
        }
    }
}