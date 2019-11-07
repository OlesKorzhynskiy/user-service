using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserService.Domain.UserAggregate
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetAsync(string id);

        Task<User> InsertAsync(User entity);

        Task<bool> UpdateAsync(User entity);

        Task DeleteAsync(string id);
    }
}