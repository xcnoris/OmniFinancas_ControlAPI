    using Modelos.Enuns;

    namespace Modelos.EF.Entidade
    {
        /// <summary>
        /// Representa uma entidade no sistema.
        /// </summary>
        public class PessoaModel
        {
            /// <summary>
            /// Identificado único da entidade.
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// Tipo da entidade (Física ou Jurídica).
            /// </summary>
            public Tipo_Entidade TipoEntidade { get; set; }
            /// <summary>
            /// Nome da entidade.
            /// </summary>
            public string Nome { get; set; }
            /// <summary>
            /// Endereço da entidade.
            /// </summary>
            public string? Endereco { get; set; }
            /// <summary>
            /// Telefone da entidade.
            /// </summary>
            public string? Telefone { get; set; }
            /// <summary>
            /// Situação da entidade (Ativa ou Inativa).
            /// </summary>
            public Situacao Situacao { get; set; }
            /// <summary>
            /// Data de criação da entidade.
            /// </summary>
            public DateTime DataCriacao { get; set; }
        }
    }
