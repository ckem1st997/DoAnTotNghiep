
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Share.BaseCore;

namespace Share.BaseCore.IRepositories
{
    public partial interface IRepositoryEF<T> where T : BaseEntity
    {
        public DbContext UnitOfWork { get; }
        /// <summary>
        /// không tạo truy vấn lưu mà chỉ tạo khi cần thiết
        /// </summary>
        public IQueryable<T> Queryable { get; }


        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        public Task<IQueryable<T>> GetByAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetList(Func<T, bool> filter = null);
        Task<T> GetFirstAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetFirstAsyncAsNoTracking(string id, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListByListId(IEnumerable<string> ids);
        void Update(T entity);
        public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        void Delete(T entity);
        void BulkInsert(IList<T> listEntity);
        Task BulkInsertAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken));
        void BulkUpdate(IList<T> listEntity);
        Task BulkUpdateAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken));
        void BulkDelete(IList<T> listEntity);
        Task<int> BulkDeleteEditOnDeleteAsync(IEnumerable<string> listEntity, CancellationToken cancellationToken = default(CancellationToken));

        Task BulkInsertOrUpdateAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken));
    }
}