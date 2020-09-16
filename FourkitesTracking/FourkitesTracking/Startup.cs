using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FourkitesTracking.Startup))]
namespace FourkitesTracking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
