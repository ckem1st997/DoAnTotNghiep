
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;
using Share.BaseCore.BaseNop;

namespace Share.BaseCore.IRepositories
{

    public interface IDapperEF : IDisposable
    {
        DbConnection GetDbconnection();
        Task<T1> QueryFirstOrDefaultAsync<T1>(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);
        T1 QueryFirst<T1>(string sp, DynamicParameters parm = null, CommandType commandType = CommandType.Text);
        Task<IEnumerable<T1>> QueryAsync<T1>(string sp, DynamicParameters parms=null, CommandType commandType = CommandType.Text);
        IEnumerable<T1> Query<T1>(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);
        Task<GridReader> QueryMultipleAsync(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);
        GridReader QueryMultiple(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);

    }


    /// <summary>
    /// nếu chạy đồng bộ thì GetQueryable và Table nhanh hơn Dapper 8 lần. 
    /// Nếu chạy bất đồng bộ thì Dapper nhanh hơn GetQueryable và Table nhanh hơn 8 lần
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IRepositoryEF<T>: IDapperEF where T : BaseEntity
    {
        public DbContext UnitOfWork { get; }
        /// <summary>
        /// không tạo truy vấn lưu mà chỉ tạo khi cần thiết nhanh hơn Table
        /// </summary>
        IQueryable<T> GetQueryable(bool tracking=false);
        IQueryable<T> Table { get; }
        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        public Task<IQueryable<T>> GetByAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetList(Func<T, bool> filter = null);
        Task<T> GetFirstAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetFirstAsyncAsNoTracking(string id, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListByListId(IEnumerable<string> ids);
        void Update(T entity);
        void Update(IEnumerable<T> entity);
        public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        void Delete(T entity);
        void Delete(IEnumerable<T> entity);
        void BulkInsert(IList<T> listEntity);
        Task BulkInsertAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken));
        void BulkUpdate(IList<T> listEntity);
        Task BulkUpdateAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken));
        void BulkDelete(IList<T> listEntity);
        Task<int> BulkDeleteEditOnDeleteAsync(IEnumerable<string> listEntity, CancellationToken cancellationToken = default(CancellationToken));

        Task BulkInsertOrUpdateAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken));


        Task<int> SaveChangesConfigureAwaitAsync(bool configure = false, CancellationToken cancellationToken = default(CancellationToken));
       
    }
}