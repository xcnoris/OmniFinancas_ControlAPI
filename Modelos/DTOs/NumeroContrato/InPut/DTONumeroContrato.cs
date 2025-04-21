namespace Modelos.DTOs.NumeroContrato.InPut
{
    public class DTONumeroContrato
    {
        public int? Id { get; set; }    
        public int ContratoId { get; set; }
        public string Numero { get; set; }

        public string NomeInstancia { get; set; }
        public string TokenInstancia { get; set; }
    }
}
