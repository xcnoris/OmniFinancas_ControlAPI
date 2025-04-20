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

        //Cada ação equivale a uma requição nos endpoints de disparos de mensagens, documentos ou relatorios
        public int QuantidadeDeAcoes { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

    }
}
