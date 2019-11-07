using System.Threading.Tasks;
using UserService.Contracts.Contracts;
using UserService.Domain;
using UserService.Domain.UserAggregate;
using UserService.Mediator.Handler;

namespace UserService.Consumer
{
    public class UserHandler :
        IHandleMessages<CreateUser>,
        IHandleMessages<UpdateUser>
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(CreateUser message)
        {
            await _userRepository.InsertAsync(new User() {Name = message.Name});
        }

        public async Task Handle(UpdateUser message)
        {
            await _userRepository.InsertAsync(new User() {Name = message.Name});
        }
    }
}