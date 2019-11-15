using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UserService.Contracts.Commands;
using UserService.Domain;
using UserService.Domain.UserAggregate;
using UserService.Mediator.Handler;

namespace UserService.Consumer
{
    public class UserHandler :
        IHandleMessages<CreateUser>,
        IHandleMessages<UpdateUser>
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<User> _userRepository;

        public UserHandler(ILogger<UserHandler> logger, IBaseRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task Handle(CreateUser message)
        {
            try
            {
                _logger.LogInformation($"Inserting a new user: {message.Name}");

                await _userRepository.InsertAsync(new User() {Id = Guid.NewGuid().ToString(), Name = message.Name});
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred inserting a new user");
            }
        }

        public async Task Handle(UpdateUser message)
        {
            try
            {
                _logger.LogInformation($"Updating user: {message.Id}");

                var user = await _userRepository.GetAsync(message.Id.ToString());
                if (user == null)
                {
                    _logger.LogError($"User with id: {message.Id} doesn't exist");
                    return;
                }

                user.Name = message.Name;

                await _userRepository.UpdateAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred updating a user");
            }
        }
    }
}