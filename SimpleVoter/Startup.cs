using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleVoter.Startup))]
namespace SimpleVoter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
