using Modelos.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.ModelosRequest.Entidade
{
    /// <summary>
    /// Classe de requisição para atualizar o tipo de uma entidade.
    /// </summary>
    public class AtualizarTipoEntidade
    {
        /// <summary>
        /// Identificador da entidade.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Novo tipo da entidade.
        /// </summary>
        public Tipo_Entidade? TipoEntidade { get; set; }
    }
}
