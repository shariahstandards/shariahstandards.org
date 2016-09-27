using Microsoft.Practices.Unity;
using System.Web.Http;
using Services;
using Unity.WebApi;

namespace WebApi
{
    public static class UnityConfig
    {
        public static IUnityContainer GetConfiguredContainer()
        {
            var container = new UnityContainer();
            container.RegisterAllServicesForWebsites();

            return container;
        }
       
    }
}