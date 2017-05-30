using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhiteBoardApplication.Startup))]
namespace WhiteBoardApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
