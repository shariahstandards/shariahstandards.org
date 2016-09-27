using System.Web.Http;
using System.Web.Http.Cors;
using Owin;
using Unity.WebApi;

namespace WebApi
{
    public class WebApiConfig
    {
        public static void Configure(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            
            config.DependencyResolver=new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});

            app.UseWebApi(config);
        }
    }
}