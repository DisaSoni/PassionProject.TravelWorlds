using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProject.TravelWorlds.Startup))]
namespace PassionProject.TravelWorlds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
