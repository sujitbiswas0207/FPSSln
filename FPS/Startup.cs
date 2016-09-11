using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPS.Startup))]
namespace FPS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
