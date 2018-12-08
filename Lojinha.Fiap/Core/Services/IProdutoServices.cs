using System.Collections.Generic;
using System.Threading.Tasks;
using Lojinha.Fiap.Core.Models;

namespace Lojinha.Fiap.Core.Services
{
    public interface IProdutoServices
    {
        Task<Produto> ObterProduto(int id);
        Task<List<Produto>> ObterProdutos();
    }
}