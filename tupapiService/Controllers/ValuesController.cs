using System.Web.Http;
using System.Web.Http.Tracing;
using Microsoft.Azure.Mobile.Server;
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
            MobileAppSettingsDictionary settings = Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            ITraceWriter traceWriter = Configuration.Services.GetTraceWriter();

            string host = settings.HostName ?? "localhost";
            string greeting = "Hello from " + host;

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