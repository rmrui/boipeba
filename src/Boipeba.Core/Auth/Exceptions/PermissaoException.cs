#pragma warning disable 1591
using System;

namespace Boipeba.Core.Auth.Exceptions
{
    /// <summary>
    /// Exception de acesso não autorizado pelo servidor à informação ou funcionalidade.
    /// </summary>
    public class PermissaoException : Exception
    {
        public PermissaoException(string msg): base(msg)
        {

        }
    }
}
