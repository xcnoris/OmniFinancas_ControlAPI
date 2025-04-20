namespace Modelos.EF.Entidade
{
    public class IdentificadorEntidadeModel
    {
        public int Id { get; set; }
        public int EntidadeId { get; set; }
        public virtual PessoaModel? Entidade { get; set; }
        public string CNPJ_CPF { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
