using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;

namespace tupapiService.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            var settings = Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            var traceWriter = Configuration.Services.GetTraceWriter();

            var host = settings.HostName ?? "localhost";
            var greeting = "Hello from " + host;

            traceWriter.Info(greeting);
            return greeting;
        }

        // POST api/values
        public string Post()
        {
            return "Hello World!";
        }
    }
}