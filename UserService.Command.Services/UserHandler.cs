using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using UserService.Command.Contracts;
using UserService.Domain;
using UserService.Domain.UserAggregate;
using UserService.Mediator.Handler;

namespace UserService.Command.Services
{
    public class UserHandler :
        IHandleMessages<CreateUser>,
        IHandleMessages<UpdateUser>
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserHandler(ILogger<UserHandler> logger, IBaseRepository<User> userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateUser message)
        {
            _logger.LogInformation($"Inserting a new user: {message.Name}");

            var user = _mapper.Map<User>(message);
            await _userRepository.InsertAsync(user);
        }

        public async Task Handle(UpdateUser message)
        {
            _logger.LogInformation($"Updating user: {message.Id}");

            var user = await _userRepository.GetAsync(message.Id.ToString());
            if (user == null)
                throw new Exception($"User with id: {message.Id} doesn't exist");

            user = _mapper.Map<User>(message);
            await _userRepository.UpdateAsync(user);
        }
    }
}