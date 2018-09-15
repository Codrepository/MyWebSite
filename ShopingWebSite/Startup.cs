using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopingWebSite.Startup))]
namespace ShopingWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
