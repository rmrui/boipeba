using System.Collections.Generic;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;
using Moq;
using NUnit.Framework;

namespace Boipeba.Tests
{
    [TestFixture]
    public class Exemplo
    {
        [Test]
        public void Test()
        {
            var repoMock = new Mock<IAssuntoRepository>();

            repoMock.Setup(x => x.Find("teste")).Returns(new List<Assunto>
            {
                new Assunto{CdAssunto = 1, DsAssunto = "teste"}
            });

            var repo = repoMock.Object;

            var list = repo.Find("teste");

            Assert.AreEqual(1, list.Count);
        }
    }
}