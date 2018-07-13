using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Services
{
    public static class ServiceRegistrationConfig
    {
        public static void RegisterAllUniqueServices(this IServiceCollection services)
        {
            var types = new List<Type>();
            types.AddRange(typeof(ServiceRegistrationConfig).Assembly.GetTypes());
            RegisterUniqueServicesForTypesList(services, types);

        }

        private static void RegisterUniqueServicesForTypesList(IServiceCollection services, List<Type> types)
        {
            var interfaces = types.Where(t => t.IsInterface && (t.Name.EndsWith("Service") || t.Name.EndsWith("Dependencies")));
            foreach (var @interface in interfaces.ToArray())
            {
                var implementations = types.Where(t =>
                    !t.IsInterface &&
                    @interface.IsAssignableFrom(t)).ToList();
                if (implementations.Count == 1)
                {
                    services.AddTransient(@interface, implementations[0]);
                }
                else
                {
                    Console.WriteLine(@interface.Name + " has " + implementations.Count + " implementations");
                }
            }
        }
    }
}
