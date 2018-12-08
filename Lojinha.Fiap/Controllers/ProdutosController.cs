using Lojinha.Fiap.Core.Models;
using Lojinha.Fiap.Core.Services;
using Lojinha.Fiap.InfraStructre.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Fiap.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly IProdutoServices _produtoServices;
        public ProdutosController(IProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }
        public IActionResult Create()
        {
            var produto = new Produto()
            {
                Id = 331868,
                Nome = "Motorola Z2",
                Categaria = new Categaria() { Id = 1, Nome = "Celular" },
                Descricao = "Motorola Z2 play",
                Fabricante = new Fabricante()
                {
                    Id = 1,
                    Nome = "Motorola"
                },
                Preco = 1200m,
                Tags = new[] { "Motorola", "Celular", "SmartPhone" },
                ImagemPrincipalUrl = "https://www.proteste.org.br/eletronicos/celular/teste/comparacao-de-celulares/motorola-moto-z2-play/31791_45629"
            };
           // _azureStorage.AddProduto(produto);


            return Content("Ok");
        }

        public async Task<IActionResult> Lista()
        {
            return Json(await _produtoServices.ObterProdutos());
        }
    }
}
