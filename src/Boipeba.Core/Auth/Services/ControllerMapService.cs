using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Boipeba.Core.Domain.Services;
using SCSI.Core.Auth;

namespace Boipeba.Core.Auth.Services
{
    /// <summary>
    /// Mapeia as roles que podem acessar os controllers para montagem de menu.
    /// </summary>
    public interface IControllerMapService : IService
    {
        /// <summary>
        /// Inicia mapeamento.
        /// </summary>
        List<ControllerMap> BuildControllerMap(List<Type> controllers);
    }

    /// <summary>
    /// Mapeia as roles que podem acessar os controllers para montagem de menu.
    /// </summary>
    public class ControllerMapService: IControllerMapService
    {
        /// <summary>
        /// Inicia mapeamento.
        /// </summary>
        /// <param name="controllers1"></param>
        public List<ControllerMap> BuildControllerMap(List<Type> controllers)
        {
            var list = new List<ControllerMap>();

            foreach (var controller in controllers)
            {
                try
                {
                    var nome = ExtractName(controller);
                    var area = ExtractArea(controller);


                    var defaultAuthorize = controller.UnderlyingSystemType.GetCustomAttributes(typeof(AuthorizeAttribute), false);
                    var customAttr = controller.UnderlyingSystemType.GetCustomAttributes(typeof(AuthorizeRoles), false);

                    var item = customAttr.Length == 0 ? defaultAuthorize : customAttr;

                    var roles = ExtractRoles((AuthorizeAttribute)item[0]);

                    list.Add(new ControllerMap(area, nome, roles));
                }
                catch
                {
                    
                }
            }

            return list;
        }

        private string ExtractArea(Type controller)
        {
            if (string.IsNullOrEmpty(controller.FullName))
                return string.Empty;

            if (!controller.FullName.Contains("Areas"))
                return string.Empty;

            var parts = controller.FullName.Split('.');

            return parts[3];
        }

        private string ExtractName(Type controller)
        {
            return controller.Name.Replace("Controller", string.Empty);
        }

        private string[] ExtractRoles(AuthorizeAttribute customAttr)
        {
            if (string.IsNullOrEmpty(customAttr.Roles)) return null;

            return customAttr.Roles.Split(',');
        }
    }
}