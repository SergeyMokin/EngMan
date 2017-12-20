[assembly: WebActivator.PostApplicationStartMethod(typeof(EngMan.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace EngMan.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using EngMan.Repository;
    using EngMan.Service;

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
            container.Register<Rule>(Lifestyle.Scoped);
            container.Register<RulesImage>(Lifestyle.Scoped);
            container.Register<RuleWithImages>(Lifestyle.Scoped);
            container.Register<EFDbContext>(Lifestyle.Scoped);
            container.Register<IRepository>(() => new RuleRepository(container.GetInstance<EFDbContext>()),Lifestyle.Scoped);
            container.Register<IService>(() => new RuleService(container.GetInstance<IRepository>()), Lifestyle.Scoped);
        }
    }
}