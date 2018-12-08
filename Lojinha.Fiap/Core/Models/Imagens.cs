using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Fiap.Core.Models
{
    public class Imagens:Entidade
    {
        public int ProdutoId { get; set; }
        public string ImegemUrl { get; set; }
    }
}
