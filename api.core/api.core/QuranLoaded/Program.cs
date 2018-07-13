using DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;

namespace QuranLoaded
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Write("press any key to start");
      Console.Read();

      WordPart wordPart = new WordPart();
      while (wordPart!=null)
      {
        var serviceProvider = GetServiceProvider();

        var quranService = serviceProvider.GetService<IQuranService>();
        try
        {
          wordPart= quranService.LoadQuranPartsFromFile(100,wordPart);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          Console.WriteLine(ex.StackTrace);
          break;
        }
      }
      var searchService = GetServiceProvider().GetService<IQuranSearchService>();
      var result = searchService.GetVerse(1, 1);
      Console.Read();
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
