using Boipeba.Core.Modulos.Cadastro;
using Boipeba.Web.Controllers;
using NHibernate;

namespace Boipeba.Web.Areas.Config.Controllers
{
    public class SpacecraftController : SimpleCrudController<Spacecraft>
    {
        public SpacecraftController(ISession session) : base(session)
        {

        }
    }
}