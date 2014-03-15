using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarcRoche.Web.Startup))]
namespace MarcRoche.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
