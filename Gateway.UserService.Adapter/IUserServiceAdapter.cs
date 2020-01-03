using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Command.Contracts;
using UserService.Query.Contracts;

namespace Gateway.UserService.Adapter
{
    public interface IUserServiceAdapter
    {
        Task Create(CreateUser command);

        Task Update(UpdateUser command);

        Task<IEnumerable<UserReadModel>> GetAll();
    }
}