#pragma warning disable 1591
using System;

namespace Boipeba.Core.Auth.Exceptions
{
    /// <summary>
    /// Exception de permissão negada.
    /// </summary>
    public class InvalidAccessException: Exception
    {
        public InvalidAccessException(): base("Permissão negada.")
        {

        }
    }
}
