using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Share.BaseCore;
using Share.BaseCore.IRepositories;
using Nest;
using System.Data.Common;
using Dapper;
using Confluent.Kafka;
using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using static Dapper.SqlMapper;

namespace Share.BaseCore.Repositories
{
    public class RepositoryEF<T> : IRepositoryEF<T>
        where T : BaseEntity, new()
    {

        //  _rep = EngineContext.Current.Resolve<IRepositoryEF<Domain.Entity.WareHouse>>(DataConnectionHelper.ConnectionStringNames.Warehouse);

        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IQueryable<T> _query;
        private readonly string _connectionstring;


        public DbContext UnitOfWork
        {
            get { return _context; }
        }

        public IQueryable<T> GetQueryable(bool tracking = false)
        {
            return tracking ? _query : _query.AsNoTracking();
        }
        public IQueryable<T> Table => _query.AsNoTracking();

        public RepositoryEF(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _query = _dbSet.AsQueryable();
            _connectionstring = _context.Database.GetConnectionString() ?? throw new ArgumentNullException("GetConnectionString is null !");
        }



        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {

            cancellationToken.ThrowIfCancellationRequested();
            // ThrowIfDisposed();
            if (entity is null)
                throw new BaseException(nameof(entity));
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public virtual void Delete(T entity)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            _dbSet.Remove(entity);
        }


        public virtual async Task<T> GetFirstAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.OnDelete);
        }

        public virtual async Task<T> GetFirstAsyncAsNoTracking(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.OnDelete);
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbSet.Where(x => !string.IsNullOrEmpty(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> ListByListId(IEnumerable<string> ids)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));
            var res = await _dbSet.Where(x => ids.ToList().Contains(x.Id)).ToListAsync();
            return res;
        }

        public virtual void Update(T entity)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            _dbSet.Update(entity);


        }

        public virtual async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            // ThrowIfDisposed();
            if (entity == null)
            {
                throw new BaseException(nameof(entity));
            }
            _dbSet.Attach(entity);
            //   entity.ConcurrencyStamp = Guid.NewGuid().ToString();
            _dbSet.Update(entity);
            try
            {
                return await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return await Task.FromResult(false);
            }
        }



        public bool AutoSaveChanges { get; set; } = true;


        protected virtual async Task<bool> SaveChanges(CancellationToken cancellationToken)
        {
            if (AutoSaveChanges)
            {
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return AutoSaveChanges;
        }
        public void BulkInsert(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new BaseException(nameof(listEntity));
            _context.BulkInsert(listEntity);
        }

        public async Task BulkInsertAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (listEntity is null)
                throw new BaseException(nameof(listEntity));
            var sb = new StringBuilder();
            await _context.BulkInsertAsync(listEntity);
        }

        public void BulkUpdate(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new BaseException(nameof(listEntity));
            _context.BulkUpdate(listEntity);
        }

        public async Task BulkUpdateAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (listEntity is null)
                throw new BaseException(nameof(listEntity));
            await _context.BulkUpdateAsync(listEntity);
        }

        public void BulkDelete(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new BaseException(nameof(listEntity));
            _context.BulkDelete(listEntity);
        }

        public async Task<int> BulkDeleteEditOnDeleteAsync(IEnumerable<string> listIds, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (listIds is null)
                throw new BaseException(nameof(listIds));
            return await _dbSet.Where(x => listIds.Contains(x.Id)).BatchUpdateAsync(x => new T { OnDelete = true });
        }

        public async Task BulkInsertOrUpdateAsync(IList<T> listEntity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (listEntity is null)
                throw new BaseException(nameof(listEntity));
            await _context.BulkInsertOrUpdateAsync(listEntity);
        }

        public IEnumerable<T> GetList(Func<T, bool> filter = null)
        {
            IEnumerable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            return query;
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _query.AsNoTracking().Where(predicate);
        }

        public async Task<IQueryable<T>> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_query.AsNoTracking().Where(predicate));
        }


        public async Task<int> SaveChangesConfigureAwaitAsync(bool configure = false, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(configure);

        }

        public async Task AddAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            await _dbSet.AddRangeAsync(entity);
        }

        public void Update(IEnumerable<T> entity)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            _dbSet.UpdateRange(entity);
        }

        public void Delete(IEnumerable<T> entity)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            _dbSet.RemoveRange(entity);
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_connectionstring);
        }

        public async Task<T1> QueryFirstOrDefaultAsync<T1>(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = new SqlConnection(_connectionstring);
            return await connection.QueryFirstOrDefaultAsync<T1>(sp, parms, commandType: commandType);
        }

        public T1 QueryFirst<T1>(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = new SqlConnection(_connectionstring);
            return connection.QueryFirst<T1>(sp, parms, commandType: commandType);
        }

        public async Task<IEnumerable<T1>> QueryAsync<T1>(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = new SqlConnection(_connectionstring);
            return await connection.QueryAsync<T1>(sp, parms, commandType: commandType);
        }     
        
        public IEnumerable<T1> Query<T1>(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = new SqlConnection(_connectionstring);
            return  connection.Query<T1>(sp, parms, commandType: commandType);
        }

        public async Task<GridReader> QueryMultipleAsync(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = new SqlConnection(_connectionstring);
            return await connection.QueryMultipleAsync(sp, param: parms, commandType: commandType);
        }


        public GridReader QueryMultiple(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = new SqlConnection(_connectionstring);
            return connection.QueryMultiple(sp, param: parms, commandType: commandType);
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    //    try {
    //    context.Add(myNewEntity);
    //   var result = context.SaveChanges();
    //    if(result==0){
    //       dbTransaction.Rollback();
    //        ... return  error
    //}
    //otherEntity.RefId = myNewEntity.Id;
    //context.Update(otherEntity);
    //// some other inserts and updates
    //result = context.SaveChanges();
    //if (result == 0)
    //{
    //    dbTransaction.Rollback();
    //    ... return error
    //     }
    //dbTransaction.Commit();
    //}
    //catch
    //{
    //    dbTransaction.Rollback();
    //    throw;
    //}

}