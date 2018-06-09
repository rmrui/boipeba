#pragma warning disable 1591

using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Boipeba.Infra
{
    /// <summary>
    /// Customização de Controller Factory para tornar o Castle Windsor responsável pelo ciclo de vida de um Controller da aplicação.
    /// </summary>
    public class WindsorControllerFactory: DefaultControllerFactory
    {
        public IWindsorContainer Container { get; protected set; }

        public WindsorControllerFactory(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("Container não configurado.");
            }

            Container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            return Container.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;

            disposableController?.Dispose();

            Container.Release(controller);
        }
    }
}