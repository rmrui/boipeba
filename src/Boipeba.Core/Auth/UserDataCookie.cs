#pragma warning disable 1591
using System;

namespace Boipeba.Core.Auth
{
    /// <summary>
    /// Value Object que será serializado em cookie criptografado e recuperado a cada request do navegador.
    /// </summary>
    [Serializable]
    public class UserDataCookie
    {
        public string Nome { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public int Matricula { get; set; }

        public string IP { get; set; }

        public bool IsMembro { get; set; }

        public string[] Roles { get; set; }

        public bool RememberMe { get; set; }
    }
}