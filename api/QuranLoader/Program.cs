using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Unity;

namespace QuranLoader
{
  class Program
  {
    static void Main(string[] args)
    {
      var container = new UnityContainer();
      container.RegisterAllUniqueServices();
      var storageService = new StorageService();
      container.RegisterInstance<IStorageService>(storageService);

      var quranService = container.Resolve<IQuranService>();
      Console.Read();

      quranService.LoadQuranFromFile();
      var searchService = new QuranSearchService();
      var result = searchService.GetVerse(1, 1);
      Console.Read();
    }
  }
}
