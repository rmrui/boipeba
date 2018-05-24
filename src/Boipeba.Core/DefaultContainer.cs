using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Domain.Repositories;
using Boipeba.Core.Domain.Services;
using Boipeba.Core.Infra.NHibernate;
using Castle.Core;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using NHibernate;
using NHibernate.Dialect.Function;
using NHibernate.Mapping.ByCode;
using Configuration = NHibernate.Cfg.Configuration;

namespace Boipeba.Core
{
    /// <summary>
    /// Implementação padrão do container da aplicação.
    /// </summary>
    public class DefaultContainer : WindsorContainer
    {
        public DefaultContainer() : base(new XmlInterpreter())
        {
            Kernel.Resolver.AddSubResolver(new ArrayResolver(Kernel));
        }

        #region Setup Methods

        /// <summary>
        /// Configura container para contexto de aplicação asp.net
        /// </summary>
        public void SetupForWeb()
        {
            if (HttpContext.Current == null)
                throw new Exception("Esta configuração é apenas para web.");

            AddFacility<StartableFacility>();
            RegisterPersistence(LifestyleType.PerWebRequest, BuildDatabaseConfiguration());
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

            Register(Component.For<ITicketProvider>().ImplementedBy<DefaultTicketProvider>().LifeStyle.Transient);

            Register(Component.For<IActiveDirectoryService>()
                .ImplementedBy<ActiveDirectoryService>()
                .DependsOn(Dependency.OnValue("adConnectionString", adConnectionString))
                .LifeStyle.Transient);

            Register(Component.For<IAuthenticationService>().ImplementedBy<AuthenticationService>().LifeStyle.Transient);

            Register(Component.For<IRoleService>().ImplementedBy<RoleService>().LifeStyle.Transient);
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
                Component.For<ISessionFactory>().UsingFactoryMethod(delegate { return config.BuildSessionFactory(); }).LifestyleSingleton(),
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession()).LifeStyle.Is(lifestyle),
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
    }

    public class CustomMsSql2012Dialect : NHibernate.Dialect.MsSql2012Dialect
    {
        public CustomMsSql2012Dialect()
        {
            RegisterFunction(
                "contains",
                new StandardSQLFunction("contains", null));
        }
    }
}