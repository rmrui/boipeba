using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Boipeba.Core.Auth;

namespace Boipeba.Web.HtmlHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class MenuHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static IHtmlString TituloMenu(this HtmlHelper helper, IPrincipal user)
        {
            if (user.IsInRole(Roles.SuperAdmin))
                return helper.Raw("NTI");

            if (user.IsInRole(Roles.Admin))
                return helper.Raw("Admin");
            
            return helper.Raw("Menu");
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool MenuDeveloper(this HtmlHelper helder, IPrincipal user)
        {
            return user.IsInRole(Roles.SuperAdmin);
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool MenuCadastros(this HtmlHelper helder, IPrincipal user)
        {
            return true;
            return user.IsInRole(Roles.Admin);
        }
    }
}