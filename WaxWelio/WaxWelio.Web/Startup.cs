using Microsoft.Owin;
using Owin;
using WaxWelio.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace WaxWelio.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
