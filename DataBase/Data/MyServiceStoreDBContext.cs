using DataBase.Data.DataBase;
using DataBase.Data.Map;

using Microsoft.EntityFrameworkCore;
using Modelos.EF;

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

        public DbSet<RevendaModel> Revenda { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfiguration(new RevendaMap());

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
