using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmpAccWebApp.Startup))]
namespace EmpAccWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
