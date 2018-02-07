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
            container.Register<EFDbContext>(Lifestyle.Scoped);
            container.Register<ISentenceTaskRepository>(() => new SentenceTaskRepository(container.GetInstance<EFDbContext>()), Lifestyle.Scoped);
            container.Register<IWordRepository>(() => new WordRepository(container.GetInstance<EFDbContext>()), Lifestyle.Scoped);
            container.Register<IRuleRepository>(() => new RuleRepository(container.GetInstance<EFDbContext>()), Lifestyle.Scoped);
            container.Register<IUserRepository>(() => new UserRepository(container.GetInstance<EFDbContext>()), Lifestyle.Scoped);
            container.Register<IMessageRepository>(() => new MessageRepository(container.GetInstance<EFDbContext>()), Lifestyle.Scoped);
            container.Register<ISentenceTaskService>(() => new SentenceTaskService(container.GetInstance<ISentenceTaskRepository>()), Lifestyle.Scoped);
            container.Register<IWordService>(() => new WordService(container.GetInstance<IWordRepository>()), Lifestyle.Scoped);
            container.Register<IRuleService>(() => new RuleService(container.GetInstance<IRuleRepository>()), Lifestyle.Scoped);
            container.Register<IUserService>(() => new UserService(container.GetInstance<IUserRepository>()), Lifestyle.Scoped);
            container.Register<IMessageService>(() => new MessageService(container.GetInstance<IMessageRepository>(), container.GetInstance<IUserService>()), Lifestyle.Scoped);
        }
    }
}