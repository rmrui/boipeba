#pragma warning disable 1591
using System;

namespace Boipeba.Core.Auth.Exceptions
{
    /// <summary>
    /// Exception de falha de comunicação com AD server.
    /// </summary>
    public class InvalidADConnection: Exception
    {
        public InvalidADConnection(Exception ex): base("Conexão com AD Server falhou. Erro de usuário e senha ou indisponibilidade de serviço.", ex)
        {
            
        }
    }
}
