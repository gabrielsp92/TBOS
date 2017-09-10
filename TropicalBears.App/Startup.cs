using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TropicalBears.App.Startup))]
namespace TropicalBears.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
