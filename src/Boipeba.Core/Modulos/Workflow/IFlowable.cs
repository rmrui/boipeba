using System;
using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Workflow
{
    public interface IFlowable: IIdentifiable
    {
        DateTime Cadastro { get; set; }
    }
}