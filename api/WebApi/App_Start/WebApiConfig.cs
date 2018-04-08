using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Unity.WebApi;
using WebApi.App_Start;

namespace WebApi
{
    public class WebApiConfig
    {
        public static void Configure(IAppBuilder app)
        {
      
            HttpConfiguration config = new HttpConfiguration();
            var formatters = config.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.DependencyResolver=new UnityDependencyResolver(UnityConfig.Container);
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
