using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF;

namespace DataBase.Data.Map
{
    public class RevendaMap : IEntityTypeConfiguration<RevendaModel>
    {
        public void Configure(EntityTypeBuilder<RevendaModel> bld)
        {
            bld.HasKey(x => x.Id);
            bld.Property(x => x.CNPJ).IsRequired().HasMaxLength(14);
            bld.Property(x => x.Nome_Empresarial).IsRequired().HasMaxLength(200);
            bld.Property(x => x.Nome_Fantasia).IsRequired().HasMaxLength(200);
            bld.Property(x => x.Endereco).IsRequired();
            bld.Property(x => x.Telefone).IsRequired();
            bld.Property(x => x.Ativo).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
        }
    }
}
