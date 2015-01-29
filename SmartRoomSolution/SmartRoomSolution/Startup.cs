using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartRoomSolution.Web.Startup))]
namespace SmartRoomSolution.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
