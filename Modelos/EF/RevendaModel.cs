namespace Modelos.EF
{
    public class RevendaModel
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string Nome_Empresarial { get; set; }
        public string Nome_Fantasia { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }


        public List<UsuariosRevendaModel>? usuariosRevendaList { get; set; }
        public List<SoftwaresModel>? SoftwaresList { get; set; }
        public List<PlanosLicencaModel>? PlanosLicencaList { get; set; }
        public List<ClientesModel>? ClientesList { get; set; }
    }
}
