using Modelos.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.ModelosRequest.Entidade
{
    /// <summary>
    /// Classe de requisição para atualizar a situação de uma entidade.
    /// </summary>
    public class AtualizarSituacaoEntidade
    {
        /// <summary>
        /// Identificador da entidade.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nova situação da entidade.
        /// </summary>
        public Situacao Situacao { get; set; }
    }
}
