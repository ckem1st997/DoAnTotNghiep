using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using System.Collections.Generic;
namespace WareHouse.API.Infrastructure.ElasticSearch
{
    public interface IElasticSearchClient<T> where T : BaseModel
    {
        public Task<bool> InsertOrUpdateAsync(T entity);
        public Task<bool> InsertOrUpdateManyAsync(IEnumerable<T> lists);
        public Task<bool> DeleteManyAsync(IEnumerable<string> ids);
        public Task<long> CountAllAsync();
    }
}
