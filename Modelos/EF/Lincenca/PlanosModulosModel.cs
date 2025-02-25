namespace Modelos.EF.Lincenca
{
    public class PlanosModulosModel
    {
        public int Id { get; set; }
        public int PlanoLicencaId { get; set; }
        public virtual PlanosLicencaModel? PlanoLicenca { get; set; }

        public int ModuloId { get; set; }
        public virtual ModulosModel? Modulo { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
