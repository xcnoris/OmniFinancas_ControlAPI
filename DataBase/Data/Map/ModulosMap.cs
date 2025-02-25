using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF;
using Modelos.EF.Lincenca;

namespace DataBase.Data.Map
{
    public class ModulosMap : IEntityTypeConfiguration<ModulosModel>
    {
        public void Configure(EntityTypeBuilder<ModulosModel> bld)
        {

            bld.HasKey(x => x.Id);
            bld.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            bld.Property(x => x.Descricao).IsRequired();
            bld.Property(x => x.SoftwareId);
            bld.Property(x => x.Situacao).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
            bld.Property(x => x.DataAtualizacao).IsRequired();

        }
    }
}
