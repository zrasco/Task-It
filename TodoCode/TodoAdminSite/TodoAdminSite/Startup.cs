using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoAdminSite.Startup))]
namespace TodoAdminSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
