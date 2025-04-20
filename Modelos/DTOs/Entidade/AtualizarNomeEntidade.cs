namespace API_Central.Controllers
{
    /// <summary>
    /// Classe de requisição para atualizar o nome de uma entidade.
    /// </summary>
    public class AtualizarNomeEntidade
    {
        /// <summary>
        /// Identificador da entidade.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Novo nome da entidade.
        /// </summary>
        public string Nome { get; set; }
    }
}
