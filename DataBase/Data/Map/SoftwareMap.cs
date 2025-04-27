using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF;

namespace DataBase.Data.Map
{
    public class SoftwareMap : IEntityTypeConfiguration<SoftwaresModel>
    {
        public void Configure(EntityTypeBuilder<SoftwaresModel> bld)
        {
            bld.HasKey(x => x.Id);
            bld.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            bld.Property(x => x.Descricao).IsRequired().HasMaxLength(1000);
            bld.Property(x => x.Versao).IsRequired();
            bld.Property(x => x.ProprietarioId).IsRequired();
            bld.Property(x => x.Situacao).IsRequired();
            bld.Property(x => x.DataCriacao).IsRequired();
            bld.Property(x => x.DataAtualizacao).IsRequired();


        }
    }
}
