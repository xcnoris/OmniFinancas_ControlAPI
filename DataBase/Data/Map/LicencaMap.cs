using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF.Lincenca;

namespace DataBase.Data.Map
{
    public class LicencaMap : IEntityTypeConfiguration<LicencaModel>
    {
        public void Configure(EntityTypeBuilder<LicencaModel> bld)
        {
            bld.HasKey(x => x.Id);
            bld.Property(x => x.ChaveAtivacao).IsRequired();
            bld.Property(x => x.ContratoId).IsRequired();
            bld.Property(x => x.QuantidadeAcoesDisponivel).IsRequired();
            bld.Property(x => x.Situacao).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
            bld.Property(x => x.DataAtualizacao).IsRequired();
        }
    }
}
