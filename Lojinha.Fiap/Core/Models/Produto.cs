using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Fiap.Core.Models
{
    public class Produto : Entidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }

        public Fabricante Fabricante { get; set; }

        public Categaria Categaria { get; set; }

        public string ImagemPrincipalUrl { get; set; }

        public Imagens[] Imagens { get; set; }

        public string[] Tags { get; set; }
    }
}
