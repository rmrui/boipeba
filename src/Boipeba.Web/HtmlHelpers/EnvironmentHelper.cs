using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace Boipeba.Web.HtmlHelpers
{
    /// <summary>
    /// Verificar a base de dados acessada e configurar a mensagem de ambiente de homologação.
    /// </summary>
    public static class EnvironmentHelper
    {
        private static string _template = "<div style='background-color:{0};width:100%;padding-bottom:5px;padding-top:5px;color:{1};font-size:{2}px;text-align:center;'>{3}</div>";

        /// <summary>
        /// Verifica modo Debug
        /// </summary>
        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        /// <summary>
        /// Constrói HTML para mensagem no topo
        /// </summary>
        public static IHtmlString TopMessage(this HtmlHelper helper)
        {
            var msg = BuildMsg();

            if (string.IsNullOrEmpty(msg)) return helper.Raw("");

            return helper.Raw(BuildTemplate(msg));
        }

        private static string BuildTemplate(string msg)
        {
            var backgroundColor = "Yellow";
            var fontColor = "Black";
            var fontSize = 10;

            return string.Format(_template, backgroundColor, fontColor, fontSize, msg);
        }

        private static string BuildMsg()
        {
            if (ConfigurationManager.ConnectionStrings["tramite"].ConnectionString.ToLower().Contains("tramite_dsv"))
            {
                return "AMBIENTE NÃO OFICIAL - DESENVOLVIMENTO";
            }

            if (ConfigurationManager.ConnectionStrings["tramite"].ConnectionString.ToLower().Contains("tramite_tst"))
            {
                return "AMBIENTE NÃO OFICIAL - HOMOLOGAÇÃO";
            }

            return string.Empty;
        }
    }
}