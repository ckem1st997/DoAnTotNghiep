
using Nest;
using Elasticsearch;
using System;
using WareHouse.API.Application.Model;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace WareHouse.API.Infrastructure.ElasticSearch
{
    public class ElasticSearchClient<T> : IElasticSearchClient<T> where T : BaseModel, new()
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchClient(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public async Task<long> CountAllAsync()
        {
            var res = await _elasticClient.CountAsync<T>();
            return res.IsValid ? res.Count : 0;

        }

        public async Task<bool> DeleteManyAsync(IEnumerable<string> ids)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }
            var res = await _elasticClient.DeleteManyAsync<T>(ids.Select(x => new T { Id = x }));
            Log.Information($"Deleted {nameof(T)} width result {res.IsValid}");
            return res.IsValid;
        }


        public async Task<bool> InsertOrUpdateAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var res = await _elasticClient.IndexDocumentAsync<T>(entity);
            Log.Information($"InsertOrUpdate {nameof(T)} width result {res.IsValid}");
            return res.IsValid;
        }

        public async Task<bool> InsertOrUpdateManyAsync(IEnumerable<T> lists)
        {
            if (lists is null)
            {
                throw new ArgumentNullException(nameof(lists));
            }
            var res = await _elasticClient.IndexDocumentAsync(lists);
            Log.Information($"InsertOrUpdateMany {nameof(T)} width result {res.IsValid}");
            return res.IsValid;
        }
    }
}
