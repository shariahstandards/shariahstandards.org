using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;

namespace Reseed
{
  class Program
  {
    static void Main(string[] args)
    {
      var serviceProvider = GetServiceProvider();

      var seederService = serviceProvider.GetService<ISeederService>();

      seederService.EnsureSeedDataPresent();
    }
    private static ServiceProvider GetServiceProvider()
    {
      var serviceCollection = new ServiceCollection();
      ServiceRegistrationConfig.RegisterAllUniqueServices(serviceCollection);
      serviceCollection.AddDbContext<DataModel.ApplicationContext>(options =>
      {
        options.UseSqlServer("Data Source=(local);Initial Catalog=ShariahStandardsCoreDev;User ID=ShariahStandardsApiUser;Password=ShariahStandardsApiUser");
        options.UseLazyLoadingProxies();
      });
      var serviceProvider = serviceCollection.BuildServiceProvider();
      return serviceProvider;
    }
  }
}
