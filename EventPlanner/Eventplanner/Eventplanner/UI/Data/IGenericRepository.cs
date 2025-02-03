using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {

        T Find(params object[] keys);
        Task<T> FindAsync(params object[] keys);
        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        List<T> ToList();
        Task<List<T>> ToListAsync();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void AddOrUpdate(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void SaveChanges();
        Task SaveChangesAsync();
        bool HasChanges();
    }
}