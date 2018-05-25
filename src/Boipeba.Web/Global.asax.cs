#pragma warning disable 1591

using Castle.Windsor;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Boipeba.Core;
using Boipeba.Core.Auth.Services;
using Boipeba.Web.Infra;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;


namespace Boipeba.Web
{
    public class MvcApplication : System.Web.HttpApplication, IContainerAccessor
    {
        private static IWindsorContainer _container;
        private static IDependencyResolver _resolver;

        protected void Application_Start()
        {
#if DEBUG
            SetupDevContainer();
#else
            SetupContainer();
#endif            

            WebApiConfig.resolver = _resolver;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
#if !DEBUG
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
#endif
        }
        
        private void SetupContainer()
        {
            _container = new DefaultContainer();

            ((DefaultContainer)_container).SetupForWeb();

            _container.Install(new WindsorControllerInstaller(_container));

            _resolver = new WindsorHttpDependencyResolver(_container);

            var windsorControllerFactory = new WindsorControllerFactory(_container);
            ControllerBuilder.Current.SetControllerFactory(windsorControllerFactory);
        }

        private void SetupDevContainer()
        {
            _container = new DevContainer();
            ((DevContainer)_container).SetupForSQLite(Castle.Core.LifestyleType.PerWebRequest);

            _container.Install(new WindsorControllerInstaller(_container));

            _resolver = new WindsorHttpDependencyResolver(_container);

            var windsorControllerFactory = new WindsorControllerFactory(_container);

            ControllerBuilder.Current.SetControllerFactory(windsorControllerFactory);
        }

        /// <summary>
        /// Responsável por identificar o usuário logado no Portal a cada request (stateless)
        /// </summary>
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            Context.User = _container.Resolve<ITicketProvider>().ResolveUser();
        }

        public IWindsorContainer Container => _container;
    }
}
