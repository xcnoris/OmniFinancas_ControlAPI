
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modelos.EF.Contrato;

public class ModulosPorNumeroMap : IEntityTypeConfiguration<ModulosPorNumeroModel>
{
    public void Configure(EntityTypeBuilder<ModulosPorNumeroModel> bld)
    {
        bld.HasKey(x => x.Id);

        bld.Property(x => x.NumeroId).IsRequired();
        bld.Property(x => x.ModuloId).IsRequired();
        bld.Property(x => x.DataCriacao).IsRequired();
        bld.Property(x => x.DataAtualizacao).IsRequired();

        bld.HasOne(x => x.Numero)
            .WithMany(x => x.Modulos)
            .HasForeignKey(x => x.NumeroId);

        bld.HasOne(x => x.Modulo)
            .WithMany()
            .HasForeignKey(x => x.ModuloId);
    }
}
