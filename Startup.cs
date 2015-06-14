using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankApp_V2_MVC.Startup))]
namespace BankApp_V2_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
