

namespace Modelos
{
    public class UploadPDFRequest
    {
        public string Licenca { get; set; }
        public string EnderecoMAC { get; set; }
        public string CNPJ { get; set; }

        public string FileName { get; set; }
        public string Base64 { get; set; }
    }
}
