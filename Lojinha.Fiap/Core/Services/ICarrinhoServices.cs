using Lojinha.Fiap.Core.Models;

namespace Lojinha.Fiap.Core.Services
{
    public interface ICarrinhoServices
    {
        void Limpar(string usuario);
        Carrinho Obter(string usuario);
        void Salvar(string usuario, Carrinho carrinho);
    }
}