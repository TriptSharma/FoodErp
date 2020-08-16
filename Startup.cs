using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodErp.Startup))]
namespace FoodErp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
