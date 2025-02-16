using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF.Revenda;

namespace DataBase.Data.Map
{
    public class UsuariosRevendaMap : IEntityTypeConfiguration<UsuariosRevendaModel>
    {
        public void Configure(EntityTypeBuilder<UsuariosRevendaModel> bld)
        {
            bld.HasKey(x => x.Id);

            // Relacionamento com a Revenda
            bld.HasOne(x => x.Revenda)
                .WithMany(x => x.UsuarioRevendaList)
                .HasForeignKey(x => x.RevendaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com a Entidade (que deve ser Física)
            bld.HasOne(x => x.Entidade)
                .WithMany()
                .HasForeignKey(x => x.EntidadeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com o login do usuário
            bld.HasOne(x => x.UsuarioLogin)
                .WithMany()
                .HasForeignKey(x => x.UsuarioLoginId)
                .OnDelete(DeleteBehavior.Cascade);

            bld.Property(x => x.Situacao).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
        }
    }
}
