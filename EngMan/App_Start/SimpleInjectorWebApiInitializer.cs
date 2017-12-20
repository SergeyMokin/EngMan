[assembly: WebActivator.PostApplicationStartMethod(typeof(EngMan.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace EngMan.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using EngMan.Repository;
    using EngMan.Service;
    using EngMan.Account;

    public static class SimpleInjectorWebApiInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IAuthProvider, FormAuthProvider>(Lifestyle.Scoped);
            container.Register<EFDbContext>(Lifestyle.Scoped);
            container.Register<IRuleRepository>(() => new RuleRepository(container.GetInstance<EFDbContext>()),Lifestyle.Scoped);
            container.Register<IRuleService>(() => new RuleService(container.GetInstance<IRuleRepository>()), Lifestyle.Scoped);
        }
    }
}