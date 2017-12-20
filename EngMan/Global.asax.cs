using System.Web.Http;
using EngMan.App_Start;

namespace EngMan
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SimpleInjectorWebApiInitializer.Initialize();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
