using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF.Contrato;

namespace DataBase.Data.Map
{
    public class ContratoMap : IEntityTypeConfiguration<ContratoModel>
    {
        public void Configure(EntityTypeBuilder<ContratoModel> bld)
        {
            bld.HasKey(x => x.Id);

            bld.Property(x => x.Tipo_PlanoId).IsRequired();
            bld.Property(x => x.ClienteFinalId).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
            bld.Property(x => x.DataAtualizacao).IsRequired();
            bld.Property(x => x.Valor).IsRequired();

            bld.HasOne(x => x.Tipo_Plano)
                .WithMany()
                .HasForeignKey(x => x.Tipo_PlanoId);

            bld.HasOne(x => x.ClienteFinal)
                .WithMany()
                .HasForeignKey(x => x.ClienteFinalId);

            bld.HasMany(x => x.NumerosDoContrato)
                .WithOne(x => x.Contrato)
                .HasForeignKey(x => x.ContratoId);
        }
    }
}