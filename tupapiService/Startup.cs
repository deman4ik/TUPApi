using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(tupapiService.Startup))]

namespace tupapiService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}