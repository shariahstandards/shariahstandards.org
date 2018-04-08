using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;

[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            var domain = $"https://{ConfigurationManager.AppSettings["Auth0Domain"]}/";
            var apiIdentifier = ConfigurationManager.AppSettings["Auth0ApiIdentifier"];

            string certificatePath = HostingEnvironment.MapPath("~/shariahstandards.cer");
            var certificate = new X509Certificate2(certificatePath);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                  AuthenticationMode = AuthenticationMode.Active,
                  TokenValidationParameters = new TokenValidationParameters()
                  {
                    ValidAudience = apiIdentifier,
                    ValidIssuer = domain,
                    IssuerSigningKeyResolver = (a, b, c, d) => { return new[] { (Microsoft.IdentityModel.Tokens.SecurityKey)(new X509SecurityKey(certificate)) }; },
                  }
                });

            // Configure Web API
            WebApiConfig.Configure(app);
      //var issuer = $"https://{ConfigurationManager.AppSettings["Auth0Domain"]}/";
      //var audience = ConfigurationManager.AppSettings["Auth0ClientID"];

      // Api controllers with an [Authorize] attribute will be validated with JWT
      //app.UseActiveDirectoryFederationServicesBearerAuthentication(
      //    new ActiveDirectoryFederationServicesBearerAuthenticationOptions
      //    {
      //        TokenValidationParameters = new TokenValidationParameters
      //        {
      //            ValidAudience = audience,
      //            ValidIssuer = issuer,
      //            IssuerSigningKeyResolver = (token, securityToken, identifier, parameters) => parameters.IssuerSigningTokens.FirstOrDefault()?.SecurityKeys?.FirstOrDefault()
      //        },
      // Setting the MetadataEndpoint so the middleware can download the RS256 certificate
      //MetadataEndpoint = $"{issuer.TrimEnd('/')}/wsfed/{audience}/FederationMetadata/2007-06/FederationMetadata.xml"
      //    });
      //// Configure Web API
      //WebApiConfig.Configure(app);




    }
    }
}
