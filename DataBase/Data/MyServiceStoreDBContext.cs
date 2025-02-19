using DataBase.Data.Map;
using Microsoft.EntityFrameworkCore;
using Modelos.EF;
using Modelos.EF.Entidade;
using Modelos.EF.Login;
using Modelos.EF.Revenda;

namespace DataBase.Data
{
    public class MyServiceStoreDBContext : DbContext
    {

        // DbSets - Tabelas do banco
        public DbSet<RevendaModel> Revendas { get; set; }
        public DbSet<UsuariosRevendaModel> UsuariosRevendas { get; set; }
        public DbSet<EntidadeModel> Entidades { get; set; }
        public DbSet<IdentificadorEntidadeModel> IdentificadoresEntidade { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }


        // Construtor que aceita DbContextOptions
        public MyServiceStoreDBContext(DbContextOptions<MyServiceStoreDBContext> options)
             : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Chama o mét1odo base para garantir a configuração padrão do Identity

            // Aplicando os mapeamentos das entidades
            modelBuilder.ApplyConfiguration(new RevendaMap());
            modelBuilder.ApplyConfiguration(new UsuariosRevendaMap());
            modelBuilder.ApplyConfiguration(new EntidadeMap());
            modelBuilder.ApplyConfiguration(new IdentificadorEntidadeMap());
            modelBuilder.ApplyConfiguration(new ClientesMap());

        }

    }
}
