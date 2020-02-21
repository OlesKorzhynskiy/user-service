using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using GrpcUserService;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using UserService.Domain.UserAggregate;

namespace UserService.Query.Grpc.Services
{
    public class UserService : UserServiceGrpc.UserServiceGrpcBase
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

        public override async Task<UsersResponse> GetAll(GetUsersRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Getting all users");

            var users = await _userRepository.GetAllAsync();

            var response = new UsersResponse();
            foreach (var user in users)
            {
                response.Users.Add(_mapper.Map<UserReadModel>(user));
            }

            return response;
        }
    }
}