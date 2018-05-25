using System.Security.Principal;

namespace Boipeba.Core.Auth
{
    /// <summary>
    /// Entidade que representa o usuario logado no Portal.
    /// </summary>
    public class Usuario: GenericPrincipal
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Usuario(IIdentity identity, string[] roles) : base(identity, roles)
        {

        }

        /// <summary>
        /// Construtor
        /// </summary>
        public Usuario(UserDataCookie userDataCookie, IIdentity identity, string[] roles) : base(identity, roles)
        {
            if (userDataCookie == null) return;

            NomeCompleto = userDataCookie.Nome;
            Login = userDataCookie.Login;
            Email = userDataCookie.Email;
            Matricula = userDataCookie.Matricula;
            IP = userDataCookie.IP;
            IsMembro = userDataCookie.IsMembro;
            Roles = userDataCookie.Roles;
        }

        /// <summary>
        /// Primeiro Nome
        /// </summary>
        public string Nome
        {
            get
            {
                if (string.IsNullOrEmpty(NomeCompleto)) return string.Empty;

                return NomeCompleto.Split(' ')[0];
            }
        }

        /// <summary>
        /// Nome Completo
        /// </summary>
        public string NomeCompleto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Matricula { get; set; }

        /// <summary>
        /// Endereço IP da conexão do usuário logado.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// True se usuário é membro do MP
        /// </summary>
        public bool IsMembro { get; set; }

        /// <summary>
        /// Roles de acesso
        /// </summary>
        public string[] Roles { get; set; }
    }
}