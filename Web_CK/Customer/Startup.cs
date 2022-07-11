using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CK_WEB.Startup))]
namespace CK_WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
