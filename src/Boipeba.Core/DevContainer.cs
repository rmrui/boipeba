using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Domain.Repositories;
using Boipeba.Core.Domain.Services;
using Boipeba.Core.Infra.NHibernate;
using Castle.Core;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace Boipeba.Core
{
    public class DevContainer : WindsorContainer
    {
        public DevContainer() : base(new XmlInterpreter())
        {

        }

        #region Setup Methods
        
        /// <summary>
        /// Configuração para qualquer host (web, console, teste) com simulação de dados.
        /// </summary>
        /// <param name="type"></param>
        public void SetupForSQLite(LifestyleType type)
        {
            AddFacility<StartableFacility>();
            RegisterPersistence(type, BuildSQLiteConfiguration());
            RegisterRepositories();
            RegisterServices();

            Register(Component.For<SQLiteSchemaService>());
            Register(Component.For<SQLiteData>());
        }

        /// <summary>
        /// Configuração para Testes unitarios ou integração. Base de dados vazia.
        /// </summary>
        public void SetupForTests()
        {
            if (HttpContext.Current != null)
                throw new Exception("Esta configuração é apenas para Testes.");

            AddFacility<StartableFacility>();
            RegisterPersistence(LifestyleType.Thread, BuildSQLiteConfiguration());

            RegisterRepositories();
            RegisterServices();
        }
        
        #endregion

        private void RegisterServices()
        {
            Register(Component.For<TransactionInterceptor>().LifeStyle.Transient);

            var adConnectionString = ConfigurationManager.ConnectionStrings["intranet"].ConnectionString;

            Register(Classes.FromAssemblyContaining<DefaultContainer>()
                .BasedOn(typeof(IService))
                .WithService.AllInterfaces()
                .Configure(x => x.Interceptors(typeof(TransactionInterceptor)))
                .Configure(c => c.LifestyleTransient()));
            
            Register(Component.For<IActiveDirectoryService>().ImplementedBy<ActiveDirectoryService>()
                .DependsOn(Dependency.OnValue("adConnectionString", adConnectionString))
                .LifeStyle.Transient);

            //Register(Component.For<IAuthenticationService>().ImplementedBy<AuthenticationService>().LifeStyle.Transient);
            //Register(Component.For<ITicketProvider>().ImplementedBy<DefaultTicketProvider>().LifeStyle.Transient);
            //Register(Component.For<IRoleService>().ImplementedBy<RoleService>().LifeStyle.Transient);
        }

        private void RegisterRepositories()
        {
            Register(Classes.FromAssemblyContaining<DefaultContainer>()
                .BasedOn(typeof(IRepository))
                .WithService.AllInterfaces()
                .Configure(x => x.Interceptors(typeof(TransactionInterceptor)))
                .Configure(c => c.LifestyleTransient()));
        }

        private void RegisterPersistence(LifestyleType lifestyle, Configuration config)
        {
            Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(delegate { return config.BuildSessionFactory(); }),
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession(), false).LifeStyle.Is(lifestyle),
                Component.For<Configuration>().Instance(config));
        }
        
        private Configuration BuildSQLiteConfiguration()
        {
            var config = new Configuration();

            config.DataBaseIntegration(x =>
            {
                x.Driver<SQLite20Driver>();
                x.Dialect<CustomSQLiteDialect>();
                x.ConnectionProvider<DriverConnectionProvider>();
                x.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                x.ConnectionString = BuildSqlitePath();
                x.Timeout = 255;
                x.BatchSize = 100;
                x.LogFormattedSql = true;
                x.LogSqlInConsole = true;
                x.AutoCommentSql = false;
            });

            var mapper = new ModelMapper();

            mapper.AddMappings(typeof(DevContainer).Assembly.GetExportedTypes());

            var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            if (domainMapping.Items != null)
                config.AddMapping(domainMapping);

            return config;
        }

        public string BuildSqlitePath()
        {
            var path = AppDomain.CurrentDomain.GetData("DataDirectory");

            var file = path == null
                ? "boipeba.db"
                : $"{path}\\boipeba.db";

            return $"Data Source={file};Version=3;New=True;";
        }

        #region SQLite Setup




        public class SQLiteSchemaService : IStartable
        {
            private readonly Configuration _config;
            private readonly GlobalSettings _settings;

            public SQLiteSchemaService(Configuration config, GlobalSettings settings)
            {
                _config = config;
                _settings = settings;
            }

            public void Start()
            {
                var path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

                if (File.Exists(path))
                    File.Delete(path);

                new SchemaExport(_config).Create(true, true);
            }

            public void Stop()
            {

            }
        }

        public class SQLiteData : IStartable
        {
            private readonly ISessionFactory _sessionFactory;

            public SQLiteData(ISessionFactory sessionFactory)
            {
                _sessionFactory = sessionFactory;
            }

            public void Start()
            {
                using (var session = _sessionFactory.OpenSession())
                {

                }
            }

            public void Stop()
            {

            }
        }
    }
    #endregion

    public class CustomSQLiteDialect : SQLiteDialect
    {
        public CustomSQLiteDialect()
        {
            RegisterColumnType(DbType.Xml, "string");
        }
    }
}