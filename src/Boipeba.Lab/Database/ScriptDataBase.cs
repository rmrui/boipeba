using Boipeba.Core;
using Castle.Core;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Boipeba.Lab.Database
{
    [TestFixture, Explicit]
    public class ScriptDataBase
    {
        [Test, Explicit]
        public void Create_Script_Database()
        {
            var container = new DevContainer();

            container.SetupForDev(LifestyleType.Transient);

            var config = container.Resolve<Configuration>();

            new SchemaExport(config).SetOutputFile("C:\\script.txt").Create(true, false);
        }
    }
}