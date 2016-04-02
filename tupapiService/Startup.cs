using Microsoft.Owin;
using Owin;
using tupapiService;

[assembly: OwinStartup(typeof (Startup))]

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