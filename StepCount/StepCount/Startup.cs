using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StepCount.Startup))]
namespace StepCount
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
