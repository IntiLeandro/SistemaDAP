using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaDAP.Startup))]
namespace SistemaDAP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
