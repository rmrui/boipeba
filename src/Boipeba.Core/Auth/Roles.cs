#pragma warning disable 1591

using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Boipeba.Core.Auth;
using Castle.Windsor;

namespace SCSI.Core.Auth
{
    public static class Roles
    {
        private static readonly AuthorizationConfig _config;

        static Roles()
        {
            var container = ((IContainerAccessor)HttpContext.Current.ApplicationInstance).Container;

            _config = container.Resolve<AuthorizationConfig>();
        }

        #region Consts

        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Servidor = "Servidor";
        public const string Membro = "Membro";

        #endregion

        #region Menu Methods

        public static bool EnsureMenuAccess(IPrincipal user, string controller, string area)
        {
            var map = _config.Map.SingleOrDefault(x => x.Nome == controller && x.Area == area);

            if (map == null) return false;
            if (user == null) return false;

            if (map.Todos) return true;
            if (map.Roles == null || map.Roles.Length == 0) return false;

            return map.Roles.Any(role => user.IsInRole(role.Trim()));
        }

        #endregion
    }
}
