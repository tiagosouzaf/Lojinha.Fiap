using Lojinha.Fiap.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lojinha.Fiap.InfraStructre.Storage
{
    public interface IAzureStorage
    {
        void AddProduto(Produto produto);
        Task<List<Produto>> ObterProdutos();
    }
}