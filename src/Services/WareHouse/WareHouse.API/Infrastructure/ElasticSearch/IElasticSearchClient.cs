﻿using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using System.Collections.Generic;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Queries.Paginated.WareHouseBook;

namespace WareHouse.API.Infrastructure.ElasticSearch
{
    /// <summary>
    /// push to queue, check connect time to 2s
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IElasticSearchClient<T> where T : BaseModel
    {
        public Task<bool> InsertOrUpdateAsync(T entity);
        public Task<bool> Ping();
        public bool InsertOrUpdate(T entity);
        public Task<bool> InsertOrUpdateManyAsync(IEnumerable<T> lists);
        public Task<bool> DeleteManyAsync(IEnumerable<string> ids);
        public Task<long> CountAllAsync();

        public Task<string> GetNameWareHouse(string id);
    }
}
