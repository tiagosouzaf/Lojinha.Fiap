using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Fiap.Core.ViewModels
{
    public class ProdutoViewModel:Dto
    {
        public string Nome { get; set; }
     
        public decimal Preco { get; set; }

        public string ImagemUrl{ get; set; }
    }
}
