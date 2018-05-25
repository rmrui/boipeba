using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Boipeba.Core.Domain.Services;

namespace Boipeba.Core.Auth.Services
{
    /// <summary>
    /// Provider que extrai e fornece o ticket/cookie de autenticação.
    /// </summary>
    public interface ITicketProvider: IService
    {
        /// <summary>
        /// Verifica informações do ticket/cookie e retorna usuário (IIdentity) logado.
        /// </summary>
        Usuario ResolveUser();

        /// <summary>
        /// Verifica informações do ticket/cookie e retorna dados do usuário logado.
        /// </summary>
        UserDataCookie ResolveUserData();

        /// <summary>
        /// Identifica ticket/cookie de acesso.
        /// </summary>
        FormsAuthenticationTicket FormsAuthenticationTicket { get; }
    }

    #region Default Impl

    /// <summary>
    /// Provider que extrai e fornece o ticket/cookie de autenticação.
    /// </summary>
    public class DefaultTicketProvider : ITicketProvider
    {
        /// <summary>
        /// Identifica ticket/cookie de acesso.
        /// </summary>
        public FormsAuthenticationTicket FormsAuthenticationTicket
        {
            get
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie == null) return null;

                return FormsAuthentication.Decrypt(authCookie.Value);
            }
        }

        /// <summary>
        /// Verifica informações do ticket/cookie e retorna dados do usuário logado.
        /// </summary>
        public UserDataCookie ResolveUserData()
        {
            if (FormsAuthenticationTicket == null) return null;

            string userData = FormsAuthenticationTicket.UserData;

            return new JavaScriptSerializer().Deserialize<UserDataCookie>(userData);
        }

        /// <summary>
        /// Verifica informações do ticket/cookie e retorna usuário (IIdentity) logado.
        /// </summary>
        public Usuario ResolveUser()
        {
            var userData = ResolveUserData();

            if (userData == null) return null;

            IIdentity id = new FormsIdentity(FormsAuthenticationTicket);

            return new Usuario(userData, id, userData.Roles);
        }
    }

    #endregion

}
