using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Boipeba.HtmlHelpers
{
    /// <summary>
    /// Helper para transformar uma instancia em um objeto Json.
    /// </summary>
    public static class JsonHtmlHelper
    {
        /// <summary>
        /// Renderizar JSON
        /// </summary>
        public static IHtmlString JsonFor(this HtmlHelper helper, object obj)
        {
            return helper.Raw(obj == null ? "{}" : new JavaScriptSerializer().Serialize(obj));
        }
    }
}