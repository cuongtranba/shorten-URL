using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shorter.Startup))]
namespace shorter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
