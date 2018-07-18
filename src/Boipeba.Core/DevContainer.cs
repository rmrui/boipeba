using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Domain.Repositories;
using Boipeba.Core.Domain.Services;
using Boipeba.Core.Infra.NHibernate;
using Boipeba.Core.Modulos.Cadastro;
using Boipeba.Core.Modulos.Processos;
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
using NHibernate.Linq.Expressions;
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
        /// Esta configuração utiliza o SQL Server.
        /// </summary>
        /// <param name="type"></param>
        public void SetupForDev(LifestyleType type)
        {
            //throw new Exception("Are you sure about this?");

            AddFacility<StartableFacility>();
            RegisterPersistence(type, BuildDatabaseConfiguration());
            RegisterRepositories();
            RegisterServices();
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

        private static Configuration BuildDatabaseConfiguration()
        {
            var config = new Configuration().Configure();

            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            if (domainMapping.Items != null)
                config.AddMapping(domainMapping);

            return config;
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
                    var player1 = new Player {Name = "Aaron Rogers", Position = "QB", Team = "Green Bay Packers"};
                    var player2 = new Player { Name = "Tom Brady", Position = "QB", Team = "New England Patriots" };

                    var ship1 = new Spacecraft {Name = "Apollo", Agency = "NASA"};
                    var ship2 = new Spacecraft { Name = "Soyuz", Agency = "Roscosmos" };

                    var ouCsi = new OrgaoUnidade {DsOrgaoUnidade = "CSI", IdOrgaoUnidade = 1, Atributos = "D"};
                    var ouCorregedoria = new OrgaoUnidade {DsOrgaoUnidade = "Corregedoria-Geral do Ministério Público", IdOrgaoUnidade = 2, Atributos = "D"};

                    var assuntoCorreicaoOrdinaria = new Assunto {CdAssunto = 930406, DsAssunto = "Correição Ordinária"};
                    var assuntoCorreicaoExtra = new Assunto {CdAssunto = 930407, DsAssunto = "Correição Extraordinária"};
                    var assuntoFerias = new Assunto {CdAssunto = 930152, DsAssunto = "Férias" };

                    var movimentoEncaminhamento = new Movimento {CdMovimento = 920025, DsMovimento = "Encaminhamento a Órgão Interno", DsMovimentoSimples = "Encaminhamento a Órgão Interno"};
                    var movimentoEntrada = new Movimento {CdMovimento = 920246, DsMovimento = "Entrada da carga", DsMovimentoSimples = "Entrada da carga"};
                    var movimentoDeferimento = new Movimento {CdMovimento = 920370, DsMovimento = "Deferido / Concedido / Procedente / Autorizado", DsMovimentoSimples = "Deferido / Concedido / Procedente / Autorizado"};
                    var movimentoIndeferimento = new Movimento {CdMovimento = 920372, DsMovimento  = "Indeferido / Prejudicado / Improcedente / Não autorizado", DsMovimentoSimples  = "Indeferido / Prejudicado / Improcedente / Não autorizado" };
                    var cienciaDecisao = new Movimento {CdMovimento = 920311, DsMovimento  = "Ciência de Decisão Administrativa" , DsMovimentoSimples  = "Ciência de Decisão Administrativa" };

                    var ruiDaBaea = new Pessoa {Matricula = 353547, Nome = "Rui Maurício", OrgaoUnidadeLotacao = ouCsi};
                    var tiagoCorreria = new Pessoa {Nome = "Tiago Magalhães", Matricula = 352862, OrgaoUnidadeLotacao = ouCorregedoria};

                    var processoCorreicao = new Processo
                    {
                        Assunto = assuntoCorreicaoOrdinaria,
                        Autor = tiagoCorreria,
                        Cadastro = DateTime.Now,
                        Complemento = "Se ligue aí",
                        OrgaoUnidadeDestino = ouCsi,
                        Interessado = ruiDaBaea.Nome
                    };

                    var movimentoEncaminhamentoCorreicao = new ProcessoMovimento
                    {
                        Autor = tiagoCorreria,
                        Data = DateTime.Now,
                        Movimento = movimentoEncaminhamento,
                        OrgaoUnidadeDestino = ouCsi,
                        OrgaoUnidadeOrigem = ouCorregedoria,
                        PessoaOrigem = tiagoCorreria,
                        Processo = processoCorreicao
                    };

                    session.Save(player1);
                    session.Save(player2);
                    session.Save(ship1);
                    session.Save(ship2);
                    session.Save(ouCsi);
                    session.Save(ouCorregedoria);
                    session.Save(assuntoCorreicaoExtra);
                    session.Save(assuntoCorreicaoOrdinaria);
                    session.Save(assuntoFerias);
                    session.Save(movimentoEncaminhamento);
                    session.Save(movimentoEntrada);
                    session.Save(movimentoDeferimento);
                    session.Save(movimentoIndeferimento);
                    session.Save(cienciaDecisao);
                    session.Save(ruiDaBaea);
                    session.Save(tiagoCorreria);
                    session.Save(processoCorreicao);
                    session.Save(movimentoEncaminhamentoCorreicao);
                    processoCorreicao.UltimoMovimento = movimentoEncaminhamentoCorreicao;
                    processoCorreicao.UltimaModificacao = movimentoEncaminhamentoCorreicao.Data;
                    session.Save(processoCorreicao);
                    session.Flush();
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