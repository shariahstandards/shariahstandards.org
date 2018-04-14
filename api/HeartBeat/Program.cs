using Services;
using StoredObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeartBeat
{
  class Program
  {
    static void Main(string[] args)
    {
      var container = new UnityContainer();
      container.RegisterAllUniqueServices();
      container.RegisterInstance<IStorageService>(new StorageService());

      var batchService = container.Resolve<IBatchJobService>();

      batchService.RunCounts();
    }
  }
}
