using Boipeba.Core.Modulos.Cadastro;
using Boipeba.Web.Controllers;
using NHibernate;

namespace Boipeba.Web.Areas.Config.Controllers
{
    public class PlayerController: SimpleCrudController<Player>
    {
        public PlayerController(ISession session) : base(session)
        {

        }
    }
}