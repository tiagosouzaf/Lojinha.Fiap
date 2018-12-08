using Lojinha.Fiap.Core.Models;
using Lojinha.Fiap.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Fiap.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly IProdutoServices _produtoServices;
        private readonly ICarrinhoServices _carrinhoService;

        public CarrinhoController(IProdutoServices produtoServices,ICarrinhoServices carrinhoServices)
        {
            _produtoServices = produtoServices;
            _carrinhoService = carrinhoServices;
        }
        public async Task<IActionResult> Add(string id)
        {
            var usuario = HttpContext.User.Identity.Name;

            Carrinho carrinho = _carrinhoService.Obter(usuario);

            carrinho.Add(await _produtoServices.ObterProduto(int.Parse(id)));

            _carrinhoService.Salvar(usuario, carrinho);

            return PartialView("Index", carrinho);
        }

        public IActionResult Finalizar()
        {
            var usuario = HttpContext.User.Identity.Name;
            var carrinho =  _carrinhoService.Obter(usuario);

            //TODO: Inserir Mensagem na Queue

             _carrinhoService.Limpar(usuario);

            return View(carrinho);
        }
    }
}
