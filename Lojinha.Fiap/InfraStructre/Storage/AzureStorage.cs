using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Lojinha.Fiap.Core.Models;
using Lojinha.Fiap.Core.Entities;

namespace Lojinha.Fiap.InfraStructre.Storage
{
    public class AzureStorage : IAzureStorage
    {
        private readonly CloudStorageAccount _account;
        private readonly CloudTableClient _tableClient;
        public AzureStorage(IConfiguration config)
        {
            _account = CloudStorageAccount.Parse(config.GetSection("Azure:Storage").Value);
            _tableClient = _account.CreateCloudTableClient();
        }

        public void AddProduto(Produto produto)
        {
            var json = JsonConvert.SerializeObject(produto);

            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var entity = new ProdutoEntity("13net", produto.Id.ToString());
            entity.Produto = json;

            TableOperation operation = TableOperation.Insert(entity);
            table.ExecuteAsync(operation).Wait();
        }

        public async Task<List<Produto>> ObterProdutos()
        {
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            TableQuery<ProdutoEntity> query = new TableQuery<ProdutoEntity>()
                .Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "13net")
                );

            TableContinuationToken token = null;

            var segment = await table.ExecuteQuerySegmentedAsync(query, token);
            var produtosEntity = segment.ToList();

            return produtosEntity
                .Where(p => p.Produto != null)
                .Select(p =>
            JsonConvert.DeserializeObject<Produto>(p.Produto)
            ).ToList();
        }

        public async Task<Produto> ObterProduto(int id)
        {
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            TableQuery<ProdutoEntity> query = new TableQuery<ProdutoEntity>()
                .Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "13net")
                )
                .Where
                (
                TableQuery.GenerateFilterCondition
                ("RowKey", QueryComparisons.Equal, id.ToString())
                );

            TableContinuationToken token = null;

            var segment = await table.ExecuteQuerySegmentedAsync(query, token);
            var produtosEntity = segment.FirstOrDefault();

            return JsonConvert.DeserializeObject<Produto>(produtosEntity.Produto);
        }
    }
}
