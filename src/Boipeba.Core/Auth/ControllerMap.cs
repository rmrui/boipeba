using System.Collections.Generic;

#pragma warning disable 1591
namespace Boipeba.Core.Auth
{
    public class AuthorizationConfig
    {
        public List<ControllerMap> Map { get; set; }
    }

    public class ControllerMap
    {
        public ControllerMap()
        {
        }

        public ControllerMap(string area, string nome, string[] roles)
        {
            Area = area;
            Nome = nome;
            Roles = roles;
            Todos = roles == null || roles.Length == 0;
        }

        public bool Todos { get; set; }

        public string Area { get; set; }

        public string Nome { get; set; }

        public string[] Roles { get; set; }

        //TODO: Criar view model ou objeto anonimo?
        public string Perfil => Roles != null? string.Join(", ", Roles): "Todos";
    }
}