using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserService.Domain
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(string id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task DeleteAsync(string id);
    }
}