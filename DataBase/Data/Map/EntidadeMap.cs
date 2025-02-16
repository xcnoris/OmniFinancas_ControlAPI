using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF.Entidade;

namespace DataBase.Data.Map
{
    public class EntidadeMap : IEntityTypeConfiguration<EntidadeModel>
    {
        public void Configure(EntityTypeBuilder<EntidadeModel> bld)
        {
            bld.HasKey(x => x.Id);

            bld.Property(x => x.TipoEntidade).IsRequired();
            bld.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            bld.Property(x => x.Endereco).HasMaxLength(300);
            bld.Property(x => x.Telefone).HasMaxLength(20);
            bld.Property(x => x.Situacao).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
        }
    }
}
