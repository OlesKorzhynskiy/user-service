using System;
using System.Threading.Tasks;
using Contracts.Contracts;
using Mediator.Handler;
using UserService.Domain.UserAggregate;

namespace Consumer
{
    public class UserHandler :
        IHandleMessages<CreateUser>,
        IHandleMessages<UpdateUser>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
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