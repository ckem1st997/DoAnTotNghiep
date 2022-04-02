using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using WareHouse.Domain.Entity;
using WareHouse.Domain.IRepositories;
using WareHouse.Domain.SeeWork;

namespace WareHouse.Infrastructure.Repositories
{
    public class RepositoryEF<T> : IRepositoryEF<T> where T : BaseEntity, new()
    {
        private readonly WarehouseManagementContext _context;
        private readonly DbSet<T> _dbSet;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public RepositoryEF(WarehouseManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>() ?? throw new ArgumentNullException(nameof(_context));
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

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity is null)
                throw new NotImplementedException(nameof(entity));
            if (string.IsNullOrEmpty(entity.Id) || entity.Id.Length < 10)
                entity.Id = Guid.NewGuid().ToString();
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public virtual void Delete(T entity)
        {
            if (entity is null)
                throw new NotImplementedException(nameof(entity));
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }


        public virtual async Task<T> GetFirstAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.OnDelete);
        }

        public virtual async Task<T> GetFirstAsyncAsNoTracking(string id)
        {
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
                throw new NotImplementedException(nameof(entity));
            //  return _dbSet.Update(entity).Entity;
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void BulkInsert(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            _context.BulkInsert(listEntity);
        }

        public async Task BulkInsertAsync(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            var sb = new StringBuilder();
            await _context.BulkInsertAsync(listEntity, options =>
            {
                options.Log= s => sb.AppendLine(s);

            });
            // Result
            Console.WriteLine("Logging Sql to Raw");
            Console.WriteLine(sb.ToString());
        }

        public void BulkUpdate(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            _context.BulkUpdate(listEntity);
        }

        public async Task BulkUpdateAsync(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            await _context.BulkUpdateAsync(listEntity);
        }

        public void BulkDelete(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            _context.BulkDelete(listEntity);
        }

        public async Task<int> BulkDeleteEditOnDeleteAsync(IEnumerable<string> listIds)
        {
            if (listIds is null)
                throw new NotImplementedException(nameof(listIds));
            return await _dbSet.Where(x => listIds.Contains(x.Id)).UpdateFromQueryAsync(x => new T { OnDelete = true });
        }

        public async Task BulkInsertOrUpdateAsync(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
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
}