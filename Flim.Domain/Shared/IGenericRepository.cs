using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Shared
{
    /// <summary>
    /// Genric repository to all db sets.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T entity);

        Task InsertRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        void UpdateRange(List<T> entities);
        Task DeleteAsync(object id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    }
}
