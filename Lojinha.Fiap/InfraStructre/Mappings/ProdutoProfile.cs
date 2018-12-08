using AutoMapper;
using Lojinha.Fiap.Core.Models;
using Lojinha.Fiap.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Fiap.InfraStructre.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(p => p.id, vm => vm.MapFrom(x => x.Id))
            .ForMember(p => p.Nome, vm => vm.MapFrom(x => x.Nome))
            .ForMember(p => p.ImagemUrl, vm => vm.MapFrom(x => x.ImagemPrincipalUrl))
            .ForMember(p => p.Preco, vm => vm.MapFrom(x => x.Preco));
        }
    }
}
