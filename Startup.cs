using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Persons_csv.Startup))]
namespace Persons_csv
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
