using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EngMan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}