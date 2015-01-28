using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartRoomSolution.Startup))]
namespace SmartRoomSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
