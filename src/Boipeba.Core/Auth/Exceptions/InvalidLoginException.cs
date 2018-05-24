#pragma warning disable 1591
using System;

namespace Boipeba.Core.Auth.Exceptions
{
    /// <summary>
    /// Exception de falha de login
    /// </summary>
    public class InvalidLoginException: Exception
    {
        public InvalidLoginException(): base("Usuário ou Senha inválido.")
        {

        }
    }
}
