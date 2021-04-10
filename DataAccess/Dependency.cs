﻿using AspNetCore.Lib.Configurations;
using AspNetCore.Lib.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Dependency
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            var resultServices = TypeRegister
              .ScanAssemblyTypes(Assembly.GetExecutingAssembly())
              .Concat(new AspNetCore.Lib.Configurations.LayerServicesTypes().GetServices(null))
              .ToList();

            resultServices.GroupBy(s => s.Lifetime).ToList().ForEach(g =>
            {
                if (g.Key == TypeLifetime.Transient)
                    g.ToList().ForEach(s =>
                    {
                        if (s.Implement != null)
                            services.AddTransient(s.BaseType, s.Implement);
                        else
                            services.AddTransient(s.BaseType, s.ImplementationType);
                    });

                else if (g.Key == TypeLifetime.Scoped)
                    g.ToList().ForEach(s =>
                    {
                        if (s.Implement != null)
                            services.AddScoped(s.BaseType, s.Implement);
                        else
                            services.AddScoped(s.BaseType, s.ImplementationType);
                    });

                else if (g.Key == TypeLifetime.Singleton)
                    g.ToList().ForEach(s =>
                    {
                        if (s.Implement != null)
                            services.AddSingleton(s.BaseType, s.Implement);
                        else if (s.Instance != null && s.BaseType != null)
                            services.AddSingleton(s.BaseType, s.Instance);
                        else if (s.Instance != null && s.BaseType == null)
                            services.AddSingleton(s.Instance);
                        else if (s.BaseType == null)
                            services.AddSingleton(s.ImplementationType);
                        else
                            services.AddSingleton(s.BaseType, s.ImplementationType);
                    });
            });


            return services;
        }
    }
}
