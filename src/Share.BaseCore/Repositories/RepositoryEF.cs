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

namespace Share.BaseCore.Repositories
{
    public class RepositoryEF<T> : IRepositoryEF<T>
        where T : BaseEntity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IQueryable<T> _query;
        public DbContext UnitOfWork
        {
            get { return _context; }
        }

        public IQueryable<T> Queryable
        {
            get { return _query; }
        }

        public RepositoryEF(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _query = _dbSet.AsQueryable();
        }


        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (records > 0 && orderBy != null)
            {
                query = orderBy(query).Take(records);
            }
            else if (orderBy != null && records == 0)
            {
                query = orderBy(query);
            }
            else if (orderBy == null && records > 0)
            {
                query = query.Take(records);
            }

            return query.AsNoTracking().ToList();
        }
        //aync

        public async Task<IEnumerable<T>> GetAync(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
          string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (records > 0 && orderBy != null)
            {
                query = orderBy(query).Take(records);
            }
            else if (orderBy != null && records == 0)
            {
                query = orderBy(query);
            }
            else if (orderBy == null && records > 0)
            {
                query = query.Take(records);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {

            cancellationToken.ThrowIfCancellationRequested();
            // ThrowIfDisposed();
            if (entity is null)
                throw new BaseException(nameof(entity));
            if (string.IsNullOrEmpty(entity.Id) || entity.Id.Length < 10)
                entity.Id = Guid.NewGuid().ToString();
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public virtual void Delete(T entity)
        {
            if (entity is null)
                throw new BaseException(nameof(entity));
            //if (_context.Entry(entity).State == EntityState.Detached)
            //{
            //    _dbSet.Attach(entity);
            //}

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
            _dbSet.Attach(entity);

            //  entity.ConcurrencyStamp = Guid.NewGuid().ToString();

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