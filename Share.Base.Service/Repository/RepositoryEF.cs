using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using static Dapper.SqlMapper;
using Share.Base.Core.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Share.Base.Core.Extensions;
using Elastic.Apm.Api;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog.Context;
using Serilog;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Share.Base.Service.Repository
{
    public class RepositoryEF<T> : IRepositoryEF<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IQueryable<T> _query;
        private readonly string _connectionstring;
        private readonly DbConnection _sqlConnection;
        public IQueryable<T> GetQueryable(bool tracking = true)
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
            if (context.Database.IsSqlServer())
                _sqlConnection = new SqlConnection(_connectionstring);
            else if (context.Database.IsOracle())
                _sqlConnection = new OracleConnection(_connectionstring);
            else
                throw new ArgumentException("Not database config !");

        }



        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {

            cancellationToken.ThrowIfCancellationRequested();
            // ThrowIfDisposed();
            if (entity is null)
                throw new BaseException(nameof(entity));
            if (entity.Id.IsNullOrEmpty())
                throw new BaseException(nameof(entity.Id));
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public virtual void Delete(T entity)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            if (entity.Id.IsNullOrEmpty())
                throw new BaseException(nameof(entity.Id));
            _dbSet.Remove(entity);
        }


        public virtual void Update(T entity)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            if (entity.Id.IsNullOrEmpty())
                throw new BaseException(nameof(entity.Id));
            _dbSet.Update(entity);
        }

        public virtual async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (entity == null)
            {
                throw new BaseException(nameof(entity));
            }
            if (entity.Id.IsNullOrEmpty())
                throw new BaseException(nameof(entity.Id));
            _dbSet.Attach(entity);
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

        public DatabaseFacade Database => _context.Database;

        protected virtual async Task<bool> SaveChanges(CancellationToken cancellationToken)
        {
            if (AutoSaveChanges)
            {
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return AutoSaveChanges;
        }

        public IEnumerable<T> GetList(Func<T, bool> filter)
        {
            IEnumerable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            return query.ToList();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _query.AsNoTracking().Where(predicate);
        }

        public async Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_query.AsNoTracking().Where(predicate));
        }


        public IQueryable<T> WhereTracking(Expression<Func<T, bool>> predicate)
        {
            return _query.Where(predicate);
        }

        public async Task<IQueryable<T>> WhereTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_query.Where(predicate));
        }


        public async Task<int> SaveChangesConfigureAwaitAsync(CancellationToken cancellationToken = default(CancellationToken), bool configure = false)
        {
            return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(configure);

        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public T SaveChangesConfigureAwaitAsync<T>(Func<T> func, CancellationToken cancellationToken = default(CancellationToken), bool configure = false)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            var strategy = _context.Database.CreateExecutionStrategy();
            var response = default(T);
            strategy.Execute(() =>
           {
               Guid transactionId;

               using (var transaction = BeginTransaction())
               using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
               {
                   Log.Information("----- Begin transaction {TransactionId}", transaction.TransactionId);
                   response = func();

                   Log.Information("----- Commit transaction {TransactionId}", transaction.TransactionId);
                   CommitTransaction(transaction, cancellationToken, configure);

                   transactionId = transaction.TransactionId;
               }

           });

            return response;
        }



        public async Task<T> SaveChangesConfigureAwaitAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default(CancellationToken), bool configure = false)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            var strategy = _context.Database.CreateExecutionStrategy();
            var response = default(T);
            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                using (var transaction = await BeginTransactionAsync())
                using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                {
                    Log.Information("----- Begin transaction {TransactionId}", transaction.TransactionId);
                    response = await func();

                    Log.Information("----- Commit transaction {TransactionId}", transaction.TransactionId);
                    await CommitTransactionAsync(transaction, cancellationToken, configure);

                    transactionId = transaction.TransactionId;
                }

            });

            return response;


        }



        public async Task AddAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            await _dbSet.AddRangeAsync(entity, cancellationToken);
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
            return _sqlConnection;
        }

        public async Task<T1> QueryFirstOrDefaultAsync<T1>(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = _sqlConnection;
            return await connection.QueryFirstOrDefaultAsync<T1>(sp, parms, commandType: commandType);
        }

        public T1 QueryFirst<T1>(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = _sqlConnection;
            return connection.QueryFirst<T1>(sp, parms, commandType: commandType);
        }

        public async Task<IEnumerable<T1>> QueryAsync<T1>(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = _sqlConnection;
            return await connection.QueryAsync<T1>(sp, parms, commandType: commandType);
        }

        public IEnumerable<T1> Query<T1>(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = _sqlConnection;
            return connection.Query<T1>(sp, parms, commandType: commandType);
        }

        public async Task<GridReader> QueryMultipleAsync(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = _sqlConnection;
            return await connection.QueryMultipleAsync(sp, param: parms, commandType: commandType);
        }


        public GridReader QueryMultiple(string sp, DynamicParameters parms, System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            using var connection = _sqlConnection;
            return connection.QueryMultiple(sp, param: parms, commandType: commandType);
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<T>> DeteleSoftDelete(IEnumerable<string> ids, CancellationToken cancellationToken = default)
        {
            if (!ids.Any())
                throw new BaseException("Danh sách mã xoá rỗng !");
            var list = await _query.Where(x => ids.Contains(x.Id) && !x.OnDelete).ToListAsync(cancellationToken: cancellationToken);
            if (!list.Any())
                throw new BaseException("Danh sách cần xoá không tồn tại !");
            list.ForEach(x =>
            {
                x.OnDelete = true;
            });
            return list;
        }

        public async Task<T> DeteleSoftDelete(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
                throw new BaseException("Mã xoá rỗng !");
            var entity = await _query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken: cancellationToken);
            if (entity == null)
                throw new BaseException("Danh sách cần xoá không tồn tại !");
            entity.OnDelete = true;
            return entity;
        }

        public async Task<T?> GetByIdsync(string id, CancellationToken cancellationToken = default, bool Tracking = false)
        {
            if (string.IsNullOrEmpty(id))
                throw new BaseException("Chưa nhập mã định danh !");
            if (!Tracking)
                return await _query.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
            return await _query.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }



        #region private

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void CommitTransaction(IDbContextTransaction transaction, CancellationToken cancellationToken, bool configure)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            try
            {
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction(transaction);
                throw;
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public void RollbackTransaction(IDbContextTransaction transaction)
        {
            try
            {

                transaction?.Rollback();
            }
            finally
            {
                transaction?.Dispose();
            }
        }




        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken, bool configure)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(configure);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransaction(transaction, cancellationToken);
                throw;
            }
            finally
            {
                if (transaction != null)
                {
                    await transaction.DisposeAsync();
                }
            }
        }

        public async Task RollbackTransaction(IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {

                if (transaction != null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }
            }
            finally
            {
                if (transaction != null)
                {
                    await transaction.DisposeAsync();
                }
            }
        }


        #endregion

    }

}