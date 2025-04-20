using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modelos.EF.Contrato;

public class NumeroContratoMap : IEntityTypeConfiguration<NumeroContratoModel>
{
    public void Configure(EntityTypeBuilder<NumeroContratoModel> bld)
    {
        bld.HasKey(x => x.Id);

        bld.Property(x => x.ContratoId).IsRequired();
        bld.Property(x => x.Numero).IsRequired().HasMaxLength(100);
        bld.Property(x => x.NomeInstancia).IsRequired().HasMaxLength(100);
        bld.Property(x => x.TokenInstancia).IsRequired().HasMaxLength(200);
        bld.Property(x => x.DataCriacao).IsRequired();
        bld.Property(x => x.DataAtualizacao).IsRequired();

        bld.HasOne(x => x.Contrato)
            .WithMany(x => x.NumerosDoContrato)
            .HasForeignKey(x => x.ContratoId);

        bld.HasMany(x => x.Modulos)
            .WithOne(x => x.Numero)
            .HasForeignKey(x => x.NumeroId);
    }
}