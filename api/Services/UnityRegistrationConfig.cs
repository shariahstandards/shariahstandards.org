using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.AspNet.Mvc;

namespace Services
{
    public static class UnityRegistrationConfig
    {
        public static void RegisterAllUniqueServices(this IUnityContainer container)
        {
            var types = new List<Type>();
            types.AddRange(typeof(UnityRegistrationConfig).Assembly.GetTypes());
            RegisterUniqueServicesForTypesList(container, types);

        }

        private static void RegisterUniqueServicesForTypesList(IUnityContainer container, List<Type> types)
        {
            var interfaces = types.Where(t => t.IsInterface && (t.Name.EndsWith("Service") || t.Name.EndsWith("Dependencies")));
            foreach (var @interface in interfaces.ToArray())
            {
                var implementations = types.Where(t =>
                    !t.IsInterface &&
                    @interface.IsAssignableFrom(t)).ToList();
                if (implementations.Count == 1)
                {
                    container.RegisterType(@interface, implementations[0]);
                }
                else
                {
                    Console.WriteLine(@interface.Name + " has " + implementations.Count + " implementations");
                }
            }
        }

        public static void RegisterAllServicesForWebsites(this IUnityContainer container)
        {
            RegisterAllUniqueServices(container);
            container.RegisterType<IStorageService, StorageService>(new PerRequestLifetimeManager());

        }
    }
}
