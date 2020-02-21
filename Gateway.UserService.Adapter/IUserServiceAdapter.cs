using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcUserService;
using UserService.Command.Contracts;
using UserReadModel = UserService.Query.Contracts.UserReadModel;

namespace Gateway.UserService.Adapter
{
    public interface IUserServiceAdapter
    {
        Task CreateAsync(CreateUser command);

        Task UpdateAsync(UpdateUser command);

        Task<IEnumerable<UserReadModel>> GetAllAsync();

        Task<UsersResponse> GetAllByGrpcAsync();
    }
}