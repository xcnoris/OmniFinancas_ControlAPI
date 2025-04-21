namespace Modelos.DTOs.NumeroContrato.InPut
{
    public class RetornoNumeroContrato
    {
        public int? Id { get; set; }
        public int ContratoId { get; set; }
        public string Numero { get; set; }
        public string NomeInstancia { get; set; }
        public string TokenInstancia { get; set; }
    }
}
