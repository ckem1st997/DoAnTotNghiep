using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Domain.Entity;
using WareHouse.Domain.SeeWork;

namespace WareHouse.Domain.IRepositories
{
    public partial interface IRepositoryEF<T> where T : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; }
        Task<T> GetFirstAsync(string id);
        Task<T> GetFirstAsyncAsNoTracking(string id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListByListId(IEnumerable<string> ids);
        void Update(T entity);
        void Delete(T entity);
        void BulkInsert(IList<T> listEntity);
        Task BulkInsertAsync(IList<T> listEntity);
        void BulkUpdate(IList<T> listEntity);
        Task BulkUpdateAsync(IList<T> listEntity);
        void BulkDelete(IList<T> listEntity);
        Task BulkDeleteAsync(IEnumerable<string> listEntity);
    }
}