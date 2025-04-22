using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF.Login;

namespace DataBase.Data.Map.Contrato
{
    internal class UserLoginMap : IEntityTypeConfiguration<UserLoginModel>
    {
        public void Configure(EntityTypeBuilder<UserLoginModel> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Nome).IsRequired();
            b.Property(x => x.Email).IsRequired();
            b.Property(x => x.HashSenha).IsRequired();
            b.Property(x => x.UsuarioRevendaId).IsRequired();
            b.Property(x => x.Situacao).IsRequired();
            b.Property(x => x.DataCriacao).IsRequired();
            b.Property(x => x.DataAtualizacao).IsRequired();
        }
    }
}
