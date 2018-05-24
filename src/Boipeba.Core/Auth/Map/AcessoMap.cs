#pragma warning disable 1591
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Auth.Map
{
    public class AcessoMap : ClassMapping<Acesso>
    {
        public AcessoMap()
        {
            Table("tAcesso");

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
                m.Column("IdAcesso");
            });

            //ManyToOne(x => x.Servidor, m =>
            //{
            //    m.Column("NuMatriculaServidor");
            //});

            //ManyToOne(x => x.Membro, m =>
            //{
            //    m.Column("NuMatriculaMembro");
            //});

            Property(x => x.Data, c => c.Column("DtAcesso"));

            Property(x => x.IP, c => c.Column("NuIP"));

            Property(x => x.RememberMe, c => c.Column("StRememberMe"));

            Property(x => x.ReturnSigned, c => c.Column("StReturnSigned"));

            Property(x => x.MobileBrowser, c => c.Column("StMobileBrowser"));
        }
    }
}
