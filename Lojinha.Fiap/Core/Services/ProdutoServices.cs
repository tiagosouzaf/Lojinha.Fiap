using Lojinha.Fiap.Core.Models;
using Lojinha.Fiap.InfraStructre.Redis;
using Lojinha.Fiap.InfraStructre.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Fiap.Core.Services
{
    public class ProdutoServices
    {
        private readonly IRedisCache _redisCache;
        private readonly IAzureStorage _azureStorage;
        public ProdutoServices(IRedisCache redisCache, IAzureStorage azureStorage)
        {
            _redisCache = redisCache;
            _azureStorage = azureStorage;
        }
        public async Task<List<Produto>> ObterProdutos()
        {
            var key = "produtos";
            var value = _redisCache.Get(key);

            if (!string.IsNullOrWhiteSpace(value))
            {
                var produtos = await _azureStorage.ObterProdutos();
                _redisCache.Set(key, JsonConvert.SerializeObject(produtos));

                return produtos;
            }

            return JsonConvert.DeserializeObject<List<Produto>>(value);
        }
    }
}
