using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.ModelosRequest.Entidade
{
    /// <summary>
    /// Ckasse de requisição para atualizar o endereço de uma entidade.
    /// </summary>
    public class AtualizarEnderecoEntidade
    {
        /// <summary>
        /// Identificador único da entidade.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Novo endereço da entidade.
        /// </summary>
        public string Endereco { get; set; }
    }
}
