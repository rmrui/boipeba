using Boipeba.Core.Auth.Exceptions;
using Boipeba.Core.Domain.Services;

namespace Boipeba.Core.Auth.Services
{
    /// <summary>
    /// Serviço fornecedor de roles de acesso do usuário.
    /// </summary>
    public interface IRoleService: IService
    {
        /// <summary>
        /// Busca roles do usuário através da matrícula
        /// </summary>
        /// <param name="id">Matricula</param>
        /// <param name="membro">Usuario membro</param>
        string[] FindRolesFor(int id, bool membro);
    }

    /// <summary>
    /// Serviço fornecedor de roles de acesso do usuário.
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly GlobalSettings _settings;

        /// <summary>
        /// Construtor
        /// </summary>
        public RoleService(GlobalSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Busca roles do usuário através da matrícula
        /// </summary>
        /// <param name="id">Matricula</param>
        /// <param name="membro">Usuário membro</param>
        public string[] FindRolesFor(int id, bool membro)
        {
            return membro? MembroRoles(id) : ServidorRoles(id);
        }

        private string[] ServidorRoles(int id)
        {
            return new[] {Roles.Servidor};
        }

        private string[] MembroRoles(int id)
        {
            return new[] { Roles.Membro };
        }
    }
}
