using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF;
using Modelos.EF.Lincenca;

namespace DataBase.Data.Map
{
    public class PlanoModuloMap : IEntityTypeConfiguration<PlanosModulosModel>
    {
        public void Configure(EntityTypeBuilder<PlanosModulosModel> bld)
        {
            bld.HasKey(x => x.Id);
            bld.Property(x => x.PlanoLicencaId).IsRequired();
            bld.Property(x => x.ModuloId).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
            bld.Property(x => x.DataAtualizacao).IsRequired();
        }
    }
}
