using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartRoom.Startup))]
namespace SmartRoom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
