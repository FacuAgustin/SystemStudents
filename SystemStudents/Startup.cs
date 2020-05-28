using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SystemStudents.Startup))]
namespace SystemStudents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
