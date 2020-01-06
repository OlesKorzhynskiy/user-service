using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Command.Contracts;
using UserService.Query.Contracts;

namespace Gateway.UserService.Adapter
{
    public interface IUserServiceAdapter
    {
        Task CreateAsync(CreateUser command);

        Task UpdateAsync(UpdateUser command);

        Task<IEnumerable<UserReadModel>> GetAllAsync();
    }
}