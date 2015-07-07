using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParentChild.Web.Startup))]
namespace ParentChild.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
