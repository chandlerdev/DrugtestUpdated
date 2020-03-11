using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransCanada.Startup))]
namespace TransCanada
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
