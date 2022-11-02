
using Nest;
using Elasticsearch;
using System;
using WareHouse.API.Application.Model;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using MediatR;
using WareHouse.API.Application.Queries.GetFisrt.WareHouses;

namespace WareHouse.API.Infrastructure.ElasticSearch
{
    public class ElasticSearchClient<T> : IElasticSearchClient<T> where T : BaseModel, new()
    {
        private readonly IElasticClient _elasticClient;
        private readonly IMediator _mediat;

        public ElasticSearchClient(IElasticClient elasticClient, IMediator mediat)
        {
            _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            _mediat = mediat;
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

        public async Task<string> GetNameWareHouse(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            }
            var getListWh = new WareHouseGetFirstCommand();
            getListWh.Id = id;
            var data = await _mediat.Send(getListWh);
            return data.Name;
        }

        public bool InsertOrUpdate(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var res = _elasticClient.IndexDocument<T>(entity);
            Log.Information($"InsertOrUpdate {nameof(T)} width result {res.IsValid}");
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
