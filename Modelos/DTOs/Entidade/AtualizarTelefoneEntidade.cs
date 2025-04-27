using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.ModelosRequest.Entidade
{
    /// <summary>
    /// Classe de requisição para atualizar o telefone de uma entidade.
    /// </summary>
    public class AtualizarTelefoneEntidade
    {
        /// <summary>
        /// Identificador da entidade.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Novo telefone da entidade.
        /// </summary>
        public string? Telefone { get; set; }
    }
}
