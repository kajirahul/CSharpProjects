using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(MMarketing.Startup))]
namespace MMarketing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            
        }
    }
}
