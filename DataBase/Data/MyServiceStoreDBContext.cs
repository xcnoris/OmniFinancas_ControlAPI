using DataBase.Data.DataBase;
using DataBase.Data.Map;
using Microsoft.EntityFrameworkCore;
using Modelos.EF;
using Modelos.EF.Entidade;
using Modelos.EF.Revenda;

namespace DataBase.Data
{
    public class MyServiceStoreDBContext : DbContext
    {
        private readonly string _connectionString;

        // Construtor que aceita DbContextOptions
        public MyServiceStoreDBContext(DbContextOptions<MyServiceStoreDBContext> options)
            : base(options)
        {
        }

        public MyServiceStoreDBContext()
        {
            string teste = "";
            var conexao = new ConexaoDB(teste);
            _connectionString = conexao.Carregarbanco();
        }

        // DbSets - Tabelas do banco
        public DbSet<RevendaModel> Revendas { get; set; }
        public DbSet<UsuariosRevendaModel> UsuariosRevendas { get; set; }
        public DbSet<EntidadeModel> Entidades { get; set; }
        public DbSet<IdentificadorEntidadeModel> IdentificadoresEntidade { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicando os mapeamentos das entidades
            modelBuilder.ApplyConfiguration(new RevendaMap());
            modelBuilder.ApplyConfiguration(new UsuariosRevendaMap());
            modelBuilder.ApplyConfiguration(new EntidadeMap());
            modelBuilder.ApplyConfiguration(new IdentificadorEntidadeMap());
            modelBuilder.ApplyConfiguration(new ClientesMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Aqui é usado a string de conexão carregada da classe ConexaoDB
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
