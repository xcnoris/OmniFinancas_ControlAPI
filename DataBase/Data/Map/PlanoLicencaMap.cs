using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF.Lincenca;

namespace DataBase.Data.Map
{
    public class PlanoLicencaMap : IEntityTypeConfiguration<PlanosLicencaModel>
    {
        public void Configure(EntityTypeBuilder<PlanosLicencaModel> bld)
        {

            bld.HasKey(x => x.Id);
            bld.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            bld.Property(x => x.DuracaoMeses).IsRequired();
            bld.Property(x => x.QuantidadeUsuarios);
            bld.Property(x => x.Situacao).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
            bld.Property(x => x.DataAtualizacao).IsRequired();

        }
    }
}
