using System;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Boipeba.Core.Auth.Exceptions;
using Boipeba.Core.Domain.Services;
using Boipeba.Core.Infra.Services;

namespace Boipeba.Core.Auth.Services
{
    /// <summary>
    /// Serviço de autenticação de usuários no portal. 
    /// A configuração do portal utiliza FormsAuthentication, que mantém dados do usuário válido em cookie/ticket criptografado.
    /// </summary>
    public interface IAuthenticationService: IService
    {
        /// <summary>
        /// Valida usuário e senha no AD, verifica as permissões de acesso e retorna matrícula se usuário estiver válido.
        /// </summary>
        /// <returns>Dados Usuario</returns>
        UserDataCookie SignIn(string username, string password, bool rememberMe, bool fallbackCaptcha, bool isMobileBrowser);
        
        /// <summary>
        /// Verifica se usuário ainda possui acesso permitido. Retorno ao portal logado com Remember Me
        /// </summary>
        //void ReValidateAccess(Usuario usuario, string IP);
    }

    /// <summary>
    /// Serviço de autenticação de usuários no portal. 
    /// A configuração do portal utiliza FormsAuthentication, que mantém dados do usuário válido em cookie/ticket criptografado.
    /// </summary>
    public class AuthenticationService: IAuthenticationService
    {
        private const string InfoCoockieName = "_ir20{0}06bu";

        private readonly IEncryptionService _encryptionService;
        private readonly IActiveDirectoryService _adService;
        private readonly IRoleService _roleService;
        private readonly ITicketProvider _ticketProvider;

        private readonly GlobalSettings _settings;

        public AuthenticationService(IActiveDirectoryService adService, 
            ITicketProvider ticketProvider,
            GlobalSettings settings, 
            IRoleService roleService, 
            IEncryptionService encryptionService)
        {
            _adService = adService;
            _settings = settings;
            _roleService = roleService;
            _ticketProvider = ticketProvider;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// Valida usuário e senha no AD, verifica as permissões de acesso e retorna matrícula se usuário estiver válido.
        /// </summary>
        /// <param name="username">
        ///     Nome de login do usuário.
        /// </param>
        /// <param name="password">
        ///     Senha do usuário, a mesma do e-mail institucional.
        /// </param>
        /// <param name="rememberMe">Permanecer autenticado.</param>
        /// <param name="fallbackCaptcha">Captcha do google bloqueado. Login utilizou o captcha fallback.</param>
        /// <param name="isMobileBrowser"></param>
        /// <returns>Dados do Usuario.</returns>
        public UserDataCookie SignIn(string username, string password, bool rememberMe, bool fallbackCaptcha, bool isMobileBrowser)
        {
            var userAd = _adService.ValidadeUser(username, password);

            var userData = userAd.BuildDataCookie();

            userData.RememberMe = rememberMe;

            var roles = ValidateAccess(userAd.Matricula, userAd.IsMembro);

            if (roles == null)
                throw new InvalidLoginException();

            SetAuthCookie(userData, roles);

            //_acessoRepository.RegistrarAcesso(userAd.Matricula, userAd.IsMembro, userData.IP, rememberMe, fallbackCaptcha, isMobileBrowser);

            return userData;
        }

        private string[] ValidateAccess(int id, bool isMembro)
        {
            if (isMembro)
            {
                //ValidateMembro(id);

                return _roleService.FindRolesFor(id, true);
            }

            var roles = _roleService.FindRolesFor(id, false);

            return roles;
        }

        private void SetAuthCookie(UserDataCookie dataCookie, string[] roles)
        {
            dataCookie.IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            dataCookie.Roles = roles;

            var ticket = CreateAuthCookie(dataCookie);

            HttpContext.Current.Response.Cookies.Add(ticket);
        }

        private HttpCookie CreateAuthCookie(UserDataCookie dataCookie)
        {
            var expiration = DateTime.Now.AddMinutes(_settings.TimeOut);
            var ispersistent = false;

            var json = new JavaScriptSerializer().Serialize(dataCookie);

            if (dataCookie.RememberMe)
            {
                ispersistent = true;
                expiration = DateTime.Now.AddYears(1);
            }

            var ticket = new FormsAuthenticationTicket(4, dataCookie.Login, DateTime.Now, expiration, ispersistent, json);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                Expires = expiration,
                Secure = FormsAuthentication.RequireSSL,
                HttpOnly = true
            };

            return cookie;
        }

        /// <summary>
        /// Verifica se usuário ainda possui acesso permitido. Retorno ao portal logado com Remember Me
        /// </summary>
        //public void ReValidateAccess(Usuario usuario, string IP)
        //{
        //    var acesso = _acessoRepository.UltimoAcesso(usuario);

        //    if (acesso != null) return;
            
        //    if (usuario.IsMembro)
        //    {
        //        var roles = ValidateAccess(usuario.Matricula, true);

        //        if (roles.Length == 0)
        //            throw new Exception("Refazer Login.");

        //        var novoAcesso = new Acesso
        //        {
        //            Data = DateTime.Now,
        //            IP = IP,
        //            ReturnSigned = true,
        //            RememberMe = false,
        //            Membro = new Membro {Id = usuario.Matricula}
        //        };

        //        _acessoRepository.Add(novoAcesso);
        //    }

        //    if (!usuario.IsExterno)
        //    {
        //        var roles = ValidateAccess(usuario.Matricula, false);

        //        if (roles.Length == 0)
        //            throw new Exception("Refazer Login.");

        //        var novoAcesso = new Acesso
        //        {
        //            Data = DateTime.Now,
        //            IP = IP,
        //            ReturnSigned = true,
        //            RememberMe = false,
        //            Servidor = new Servidor {Id = usuario.Matricula}
        //        };

        //        _acessoRepository.Add(novoAcesso);
        //    }

        //    if (usuario.IsExterno)
        //    {
        //        throw new Exception("Refazer Login.");
        //    }
        //}
    }
}
