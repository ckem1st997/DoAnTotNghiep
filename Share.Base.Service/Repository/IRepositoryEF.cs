
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
using Share.Base.Core.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Share.Base.Service.Repository
{

    public interface IDapperEF : IDisposable
    {
        DbConnection GetDbconnection();
        Task<T1> QueryFirstOrDefaultAsync<T1>(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);
        T1 QueryFirst<T1>(string sp, DynamicParameters parm = null, CommandType commandType = CommandType.Text);
        Task<IEnumerable<T1>> QueryAsync<T1>(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);
        IEnumerable<T1> Query<T1>(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);
        Task<GridReader> QueryMultipleAsync(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);
        GridReader QueryMultiple(string sp, DynamicParameters parms = null, CommandType commandType = CommandType.Text);

    }


    /// <summary>
    /// nếu chạy đồng bộ thì GetQueryable và Table nhanh hơn Dapper 8 lần. 
    /// Nếu chạy bất đồng bộ thì Dapper nhanh hơn GetQueryable và Table nhanh hơn 8 lần
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IRepositoryEF<T> : IDapperEF where T : BaseEntity
    {
        /// <summary>
        /// không tạo truy vấn lưu mà chỉ tạo khi cần thiết nhanh hơn Table
        /// </summary>
        IQueryable<T> GetQueryable(bool tracking = true);
        IQueryable<T> Table { get; }
        DatabaseFacade Database { get; }
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        public Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> predicate);

        public IQueryable<T> WhereTracking(Expression<Func<T, bool>> predicate);
        public Task<IQueryable<T>> WhereTrackingAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdsync(string id, CancellationToken cancellationToken = default(CancellationToken), bool Tracking = false);
        IEnumerable<T> GetList(Func<T, bool> filter);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// hàm này sẽ update tất cả các trường, nếu muốn tận dụng tracking thì hãy bỏ update và dùng savechange
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        void Update(IEnumerable<T> entity);
        public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        void Delete(T entity);
        void Delete(IEnumerable<T> entity);
        Task<int> SaveChangesConfigureAwaitAsync(CancellationToken cancellationToken = default(CancellationToken),bool configure = false);
        Task<IEnumerable<T>> DeteleSoftDelete(IEnumerable<string> ids, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> DeteleSoftDelete(string id, CancellationToken cancellationToken = default(CancellationToken));
    }
}