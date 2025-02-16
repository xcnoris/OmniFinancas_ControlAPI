using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF;

namespace DataBase.Data.Map
{
    public class ClientesMap : IEntityTypeConfiguration<ClientesModel>
    {
        public void Configure(EntityTypeBuilder<ClientesModel> bld)
        {
            bld.HasKey(x => x.Id);

            // Relacionamento com Entidade
            bld.HasOne(x => x.Entidade)
                .WithMany()
                .HasForeignKey(x => x.EntidadeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Revenda
            bld.HasOne(x => x.Revenda)
                .WithMany(x => x.ClientesList)
                .HasForeignKey(x => x.RevendaId)
                .OnDelete(DeleteBehavior.Restrict);

            bld.Property(x => x.Situacao).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
        }
    }
}
