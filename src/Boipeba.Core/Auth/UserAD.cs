#pragma warning disable 1591
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using Boipeba.Core.Auth.Exceptions;

namespace Boipeba.Core.Auth
{
    /// <summary>
    /// Entidade de mapeamento do Active Directory para obtenção de dados do usuário.
    /// </summary>
    public class UserAD
    {
        public UserAD()
        {
        }

        public UserAD(UserPrincipal user)
        {
            if (user == null)
                throw new InvalidLoginException();

            int mat;

            if (!int.TryParse(user.Matricula(), out mat))
                throw new InvalidMatriculaException();

            DisplayName = user.DisplayName;
            Nome = user.GivenName;
            Sobrenome = user.Surname;
            Email = user.EmailAddress;
            NomeCompleto = user.Name;
            Matricula = mat;
            Login = user.SamAccountName;

            Parse(user.DistinguishedName);
        }

        public string DisplayName { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public int Matricula { get; set; }

        public string Login { get; set; }

        public List<string> OU { get; set; }

        public bool IsServidor
        {
            get { return OU.Count(x => x.Contains("Procuradores_Promotores")) == 0; }
        }

        public bool IsMembro
        {
            get { return OU.Count(x => x.Contains("Procuradores_Promotores")) > 0; }
        }

        public void Parse(string line)
        {
            OU = new List<string>();

            var parts = line.Split(',');

            var oulist = parts.Where(x => x.StartsWith("OU=")).ToList();
            
            foreach (var item in oulist)
            {
                OU.Add(item);
            }
        }

        public override string ToString()
        {
            return $"{NomeCompleto}, {Matricula}, {Login}, {OU}";
        }

        public UserDataCookie BuildDataCookie()
        {
            return new UserDataCookie { Login = Login, Email = Email, Matricula = Matricula, Nome = NomeCompleto, IsMembro = IsMembro};
        }
    }

    public static class AccountManagementExtensions
    {
        public static string GetProperty(this Principal principal, string property)
        {
            var directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;

            return directoryEntry != null && directoryEntry.Properties.Contains(property) 
                ? directoryEntry.Properties[property].Value.ToString() 
                : string.Empty;
        }

        public static string Matricula(this Principal principal)
        {
            return principal.GetProperty("wWWHomePage");
        }
    }
}
