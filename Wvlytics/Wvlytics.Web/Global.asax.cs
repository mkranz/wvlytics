using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Wvlytics.Config;

namespace Wvlytics.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private IContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            _container = IoC.Init();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }
    }

    internal class IoC
    {
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            SetupCore(builder);

            SetupWebApi(builder, GlobalConfiguration.Configuration);
            SetupMvc(builder);
            var container = builder.Build();

            return container;
        }

        private static void SetupMvc(ContainerBuilder builder)
        {
            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();
        }

        private static void SetupWebApi(ContainerBuilder builder, HttpConfiguration config)
        {
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);
        }

        private static void SetupCore(ContainerBuilder builder)
        {
            builder.RegisterModule<WvlyticsCoreModule>();
            //builder.RegisterModule<DynamoModule>();
            builder.RegisterModule<LocalFileModule>();
        }
    }
}