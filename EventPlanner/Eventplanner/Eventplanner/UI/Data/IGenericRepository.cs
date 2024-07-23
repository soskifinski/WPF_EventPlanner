using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public interface IGenericRepository<TEntity> 
    {
        void Add(TEntity model);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        bool HasChanges();
        void Remove(TEntity model);
        Task SaveAsync();
    }
}