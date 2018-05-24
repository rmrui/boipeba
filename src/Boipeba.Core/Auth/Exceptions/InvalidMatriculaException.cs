#pragma warning disable 1591
using System;

namespace Boipeba.Core.Auth.Exceptions
{
    /// <summary>
    /// Exception de problemas com a busca de matrícula nos dados cadastrados no AD server.
    /// </summary>
    public class InvalidMatriculaException: Exception
    {
        public InvalidMatriculaException() : base("Matrícula não cadastrada no AD Server.")
        {
        }
    }
}