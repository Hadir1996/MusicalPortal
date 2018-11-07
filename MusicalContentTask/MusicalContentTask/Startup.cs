using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicalContentTask.Startup))]
namespace MusicalContentTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
