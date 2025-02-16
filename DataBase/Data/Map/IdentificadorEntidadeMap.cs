using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF.Entidade;

namespace DataBase.Data.Map
{
    public class IdentificadorEntidadeMap : IEntityTypeConfiguration<IdentificadorEntidadeModel>
    {
        public void Configure(EntityTypeBuilder<IdentificadorEntidadeModel> bld)
        {
            bld.HasKey(x => x.Id);

            // Relacionamento com Entidade
            bld.HasOne(x => x.Entidade)
                .WithMany()
                .HasForeignKey(x => x.EntidadeId)
                .OnDelete(DeleteBehavior.Cascade);

            bld.Property(x => x.CNPJ_CPF).IsRequired().HasMaxLength(18);
            bld.Property(x => x.DataCriacao).IsRequired();
        }
    }
}
