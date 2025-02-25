using DataBase.Data.Map;
using Microsoft.EntityFrameworkCore;
using Modelos.EF;
using Modelos.EF.Entidade;
using Modelos.EF.Lincenca;
using Modelos.EF.Revenda;

namespace DataBase.Data
{
    public class MyServiceStoreDBContext : DbContext
    {

        // DbSets - Tabelas do banco
        public DbSet<RevendaModel> Revendas { get; set; }
        public DbSet<UsuariosRevendaModel> Usuarios_Revendas { get; set; }
        public DbSet<EntidadeModel> Entidades { get; set; }
        public DbSet<IdentificadorEntidadeModel> Identificadores_Entidade { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<SoftwaresModel> Softwares { get; set; }
        public DbSet<PlanosLicencaModel> Planos_Licenca { get; set; }
        public DbSet<LicencaModel> Licencas { get; set; }
        public DbSet<ModulosModel> Modulos_Software { get; set; }
        public DbSet<PlanosModulosModel> Planos_Modulos { get; set; }


        // Construtor que aceita DbContextOptions
        public MyServiceStoreDBContext(DbContextOptions<MyServiceStoreDBContext> options)
             : base(options) { }



        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb); // Chama o mét1odo base para garantir a configuração padrão do Identity

            // Aplicando os mapeamentos das entidades
            mb.ApplyConfiguration(new RevendaMap());
            mb.ApplyConfiguration(new UsuariosRevendaMap());
            mb.ApplyConfiguration(new EntidadeMap());
            mb.ApplyConfiguration(new IdentificadorEntidadeMap());
            mb.ApplyConfiguration(new ClientesMap());
            mb.ApplyConfiguration(new SoftwareMap());
            mb.ApplyConfiguration(new PlanoLicencaMap());
            mb.ApplyConfiguration(new LicencaMap());
            mb.ApplyConfiguration(new ModulosMap());
            mb.ApplyConfiguration(new PlanoModuloMap());
        }
    }
}
