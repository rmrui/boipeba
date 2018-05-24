using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Boipeba.Core.Auth;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Domain.Controller;
using Boipeba.Web.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Boipeba.Web.Infra
{
    /// <summary>
    /// Registra todos os controllers da aplicação no Windsor Container
    /// </summary>
    public class WindsorControllerInstaller : IWindsorInstaller
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public WindsorControllerInstaller(IWindsorContainer container)
        {
            _container = container;

        }

        /// <summary>
        /// Registra todos os controllers da aplicação.
        /// </summary>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var controllers = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.BaseType == typeof(BaseController))
                .ToList();

            SetupControllerMap(controllers);

            container.Register(Classes.FromAssemblyContaining<HomeController>()
                 .BasedOn(typeof(IBoipebaController))
                 .Configure(c => c.LifestylePerWebRequest()));
        }

        private void SetupControllerMap(List<Type> controllers)
        {
            var service = _container.Resolve<IControllerMapService>();

            var map = new AuthorizationConfig
            {
                Map = service.BuildControllerMap(controllers)
            };

            _container.Register(Component.For<AuthorizationConfig>().Instance(map).LifestyleSingleton());
        }
    }
}