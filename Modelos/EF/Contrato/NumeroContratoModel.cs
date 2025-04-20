namespace Modelos.EF.Contrato
{
    public class NumeroContratoModel
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public virtual ContratoModel? Contrato { get; set; }
        public string Numero { get; set; }

        public string NomeInstancia { get; set; }
        public string TokenInstancia { get; set; }


        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public List<ModulosPorNumeroModel> Modulos = new List<ModulosPorNumeroModel>();
    }
}
