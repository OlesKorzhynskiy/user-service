using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Query.Contracts;

namespace UserService.Query.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserReadModel>> GetAsync();
    }
}