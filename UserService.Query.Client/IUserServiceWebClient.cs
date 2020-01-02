using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using UserService.Query.Contracts;

namespace UserService.Query.Client
{
    public interface IUserServiceWebClient
    {
        [Get("/api/users")]
        Task<IEnumerable<UserReadModel>> GetAll();
    }
}