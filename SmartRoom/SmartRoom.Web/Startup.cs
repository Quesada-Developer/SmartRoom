using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartRoom.Web.Startup))]
namespace SmartRoom.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
