using System.Web.Http;
using System.Web.Http.Dependencies;

#pragma warning disable 1591


namespace Boipeba.Web
{
    /// <summary>
    /// Configuração das rotas de WebAPI da aplicação.
    /// 
    /// NÃO UTILIZADA AINDA.
    /// </summary>
    public static class WebApiConfig
    {
        public static IDependencyResolver resolver;

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.DependencyResolver = resolver;
        }
    }
}
