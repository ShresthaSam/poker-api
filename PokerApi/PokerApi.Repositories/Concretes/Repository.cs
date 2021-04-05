using Microsoft.EntityFrameworkCore;
using PokerApi.Repositories.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ILogger _logger;
        private readonly DbContext _context;
        public Repository(
            ILogger logger,
            DbContext context)
        {
            _logger = logger.ForContext("SourceContext", nameof(Repository<T>));
            _context = context;
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            _logger.Debug("Entity Added");
        }

        public async Task Add(params T[] entities)
        {
            await _context.Set<T[]>().AddRangeAsync(entities);
            _logger.Debug("Entities Added");
        }

        public async Task Add(IEnumerable<T> entities)
        {
            await _context.Set<IEnumerable<T>>().AddRangeAsync(entities);
            _logger.Debug("Entities Added");
        }

        public async Task Delete(T entity)
        {
            T existing = await _context.Set<T>().FindAsync(entity);
            if (existing != null)
            {
                _context.Set<T>().Remove(existing);
                _logger.Debug("Entity Deleted");
            } else
            {
                _logger.Debug("Entity not found and cannot be deleted");
            }
        }

        public async Task Delete(object id)
        {
            object existing = await _context.Set<object>().FindAsync(id);
            if (existing != null)
            {
                _context.Set<object>().Remove(existing);
                _logger.Debug("Entity Deleted");
            }
            else
            {
                _logger.Debug("Entity not found and cannot be deleted");
            }
        }

        public async Task Delete(params T[] entities)
        {
            foreach (T entity in entities)
            {
                T existing = await _context.Set<T>().FindAsync(entity);
                if (existing != null)
                {
                    _context.Set<T>().Remove(existing);
                    _logger.Debug("Entity Deleted");
                }
                else
                {
                    _logger.Debug("Entity not found and cannot be deleted");
                }
            }
        }

        public async Task Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                T existing = await _context.Set<T>().FindAsync(entity);
                if (existing != null)
                {
                    _context.Set<T>().Remove(existing);
                    _logger.Debug("Entity Deleted");
                }
                else
                {
                    _logger.Debug("Entity not found and cannot be deleted");
                }
            }
        }

        public System.Linq.IQueryable<T> Query(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public T Search(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Single(
            System.Linq.Expressions.Expression<Func<T, bool>> predicate = null, 
            Func<System.Linq.IQueryable<T>, 
            System.Linq.IOrderedQueryable<T>> orderBy = null, 
            Func<System.Linq.IQueryable<T>, 
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>> include = null, 
            bool disableTracking = true)
        {
            throw new NotImplementedException();

        }

        public async Task<T> Single(
            System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            var queryable = await _context.Set<T>().SingleOrDefaultAsync(predicate);
            return queryable;
        }

        public IQueryable<T> GetAll()
        {
            var queryable = _context.Set<T>().AsQueryable<T>();
            return queryable;
        }

        public IQueryable<T> Get(
            System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            var queryable = _context.Set<T>().Where(predicate);
            return queryable;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Attach(entity);
            _logger.Debug("Entity Updated");
        }

        public void Update(params T[] entities)
        {
            _context.Entry(entities).State = EntityState.Modified;
            _context.Set<T[]>().AttachRange(entities);
            _logger.Debug("Entities Updated");
        }

        public void Update(IEnumerable<T> entities)
        {
            _context.Entry(entities).State = EntityState.Modified;
            _context.Set<IEnumerable<T>>().AttachRange(entities);
            _logger.Debug("Entities Updated");
        }

        #region Disposable pattern
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
