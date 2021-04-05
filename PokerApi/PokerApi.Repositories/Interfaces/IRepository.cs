using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PokerApi.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> Query(string sql, params object[] parameters);

        T Search(params object[] keyValues);

        T Single(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        Task<T> Single(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);

        Task Add(T entity);
        Task Add(params T[] entities);
        Task Add(IEnumerable<T> entities);

        Task Delete(T entity);
        Task Delete(object id);
        Task Delete(params T[] entities);
        Task Delete(IEnumerable<T> entities);

        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
    }
}
