using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlayListApp.Startup))]
namespace PlayListApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
