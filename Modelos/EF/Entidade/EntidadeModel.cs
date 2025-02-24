    using Modelos.Enuns;

    namespace Modelos.EF.Entidade
    {
        public class EntidadeModel
        {
            public int Id { get; set; }
            public Tipo_Entidade TipoEntidade { get; set; }
            public string Nome { get; set; }
            public string? Endereco { get; set; }
            public string? Telefone { get; set; }
            public Situacao Situacao { get; set; }
            public DateTime DataCriacao { get; set; }
        }
    }
