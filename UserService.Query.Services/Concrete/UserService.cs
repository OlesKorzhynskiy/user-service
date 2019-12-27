using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using UserService.Domain.UserAggregate;
using UserService.Query.Contracts;
using UserService.Query.Services.Interfaces;

namespace UserService.Query.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserReadModel>> GetAsync()
        {
            _logger.LogInformation($"Getting all users");

            return _mapper.Map<List<UserReadModel>>(await _userRepository.GetAllAsync());
        }
    }
}