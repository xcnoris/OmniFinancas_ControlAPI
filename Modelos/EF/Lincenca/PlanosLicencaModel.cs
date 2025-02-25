using Modelos.Enuns;

namespace Modelos.EF.Lincenca
{
    public class PlanosLicencaModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Nome do plano de licença.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Duração do plano em meses (0 para vitalício).
        /// </summary>
        public int DuracaoMeses { get; set; }

        /// <summary>
        /// Número máximo de usuários permitidos na licença.
        /// </summary>
        public int QuantidadeUsuarios { get; set; }

        /// <summary>
        /// Situação do plano de licença.
        /// </summary>
        public Situacao Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }


        public virtual ICollection<PlanosModulosModel>? ModulosPermitidos { get; set; }

    }
}
