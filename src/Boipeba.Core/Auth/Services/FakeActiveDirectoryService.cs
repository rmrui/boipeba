using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using Boipeba.Core.Auth.Exceptions;

namespace Boipeba.Core.Auth.Services
{
    /// <summary>
    /// Serviço de acesso à informçaões do Active Directory
    /// </summary>
    public class FakeActiveDirectoryService : IActiveDirectoryService
    {
        private readonly string _adConnectionString;
        private readonly GlobalSettings _settings;

        public FakeActiveDirectoryService(string adConnectionString, GlobalSettings settings)
        {
            _adConnectionString = adConnectionString;
            _settings = settings;
        }

        /// <summary>
        /// Valida usuário e senha
        /// </summary>
        /// <returns>Usuário AD</returns>
        //TODO[Refatorar] usuario membro ficticio
        public UserAD ValidadeUser(string username, string password)
        {
            return Find(username, password);
        }

        /// <summary>
        /// Serviço de acesso à informçaões do Active Directory
        /// </summary>
        public UserAD Find(string username, string password)
        {
            return new UserAD
            {
                Login = "Developer",
                Nome = "Developer",
                OU = new List<string> { "Funcionarios" },
                Matricula = 353547,
                DisplayName = "Developer",
                Email = "teste@teste.com",
                NomeCompleto = "Developer",
                Sobrenome = "X"
            };
        }
    }
}
