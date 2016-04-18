using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using tupapiService.Helpers.DBHelpers;

namespace tupapiService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();
            config.MapHttpAttributeRoutes();
            new MobileAppConfiguration()
                //  .AddMobileAppHomeController()
                .UseDefaultConfiguration()
                //  .MapLegacyCrossDomainController()
                .ApplyTo(config);


            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new TupapiInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            //Database.SetInitializer<TupapiContext>(null);

            var settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] {ConfigurationManager.AppSettings["ValidAudience"]},
                    ValidIssuers = new[] {ConfigurationManager.AppSettings["ValidIssuer"]},
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
            ConfigureSwagger(config);
        }
    }
}