using Microsoft.Owin;
using Owin;
using Microsoft.AspNetCore.StaticFiles;

[assembly: OwinStartupAttribute(typeof(Portfolio.Startup))]
namespace Portfolio
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
