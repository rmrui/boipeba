using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using Boipeba.Core.Auth.Exceptions;
using SCSI.Core.Auth;

namespace Boipeba.Core.Auth.Services
{
    /// <summary>
    /// Serviço de acesso à informçaões do Active Directory
    /// </summary>
    public interface IActiveDirectoryService
    {
        /// <summary>
        /// Valida usuário e senha
        /// </summary>
        /// <returns>Usuário AD</returns>
        UserAD ValidadeUser(string username, string password);

        /// <summary>
        /// Busca dados do usuário AD
        /// </summary>
        /// <returns>Usuário AD</returns>
        UserAD Find(string username, string password);
    }

    /// <summary>
    /// Serviço de acesso à informçaões do Active Directory
    /// </summary>
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private readonly string _adConnectionString;
        private readonly GlobalSettings _settings;

        public ActiveDirectoryService(string adConnectionString, GlobalSettings settings)
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
#if DEBUG
            var x = false;

            if (username.EndsWith(".x"))
            {
                x = true;
                username = username.Replace(".x", "");
            }

            if (username.Equals("developer") && password.Equals("dev@123"))
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
#endif

            using (var context = new PrincipalContext(ContextType.Domain, _adConnectionString, username, password))
            {
                var valid = context.ValidateCredentials(username, password);
#if DEBUG
                if (x) username = _settings.FakeLogin;
#endif
                if (!valid)
                    throw new InvalidLoginException();

                var user = RetrieveUserAD(username, context);
                
                return user;
            }
        }

        /// <summary>
        /// Serviço de acesso à informçaões do Active Directory
        /// </summary>
        public UserAD Find(string username, string password)
        {
            using (var context = new PrincipalContext(ContextType.Domain, _adConnectionString, username, password))
            {
                var user = RetrieveUserAD(username, context);

                return user;
            }
        }

        private UserAD RetrieveUserAD(string username, PrincipalContext context)
        {
            var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);

            return new UserAD(userPrincipal);
        }
    }
}
