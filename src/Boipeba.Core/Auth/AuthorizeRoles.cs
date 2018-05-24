#pragma warning disable 1591
using System.Web.Mvc;

namespace Boipeba.Core.Auth
{
    /// <summary>
    /// Especialização para permitir a referencia de multiplas roles na configuração de autorização.
    /// </summary>
    public class AuthorizeRoles : AuthorizeAttribute
    {
        public AuthorizeRoles(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}