using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using UserService.Command.Contracts;
using UserService.Mediator.Dispatcher;
using UserService.Query.Client;
using Grpc.Net.Client;
using GrpcUserService;
using Microsoft.Extensions.Configuration;
using UserReadModel = UserService.Query.Contracts.UserReadModel;

namespace Gateway.UserService.Adapter
{
    public class UserServiceAdapter : IUserServiceAdapter
    {
        private readonly IDispatcher _dispatcher;
        private readonly IUserServiceWebClient _userServiceWebClient;
        private readonly ILogger<UserServiceAdapter> _logger;
        private readonly string _grpcUrl;

        public UserServiceAdapter(IDispatcher dispatcher, IUserServiceWebClient userServiceWebClient, ILogger<UserServiceAdapter> logger, IConfiguration configuration)
        {
            _dispatcher = dispatcher;
            _userServiceWebClient = userServiceWebClient;
            _logger = logger;
            _grpcUrl = configuration["Services:UserService:GrpcUrl"];
        }

        public async Task CreateAsync(CreateUser command)
        {
            await _dispatcher.Dispatch(command, Handler);
        }

        public async Task UpdateAsync(UpdateUser command)
        {
            await _dispatcher.Dispatch(command, Handler);
        }

        public Task<IEnumerable<UserReadModel>> GetAllAsync()
        {
            return _userServiceWebClient.GetAll();
        }

        public async Task<UsersResponse> GetAllByGrpcAsync()
        {
            var channel = GrpcChannel.ForAddress(_grpcUrl);

            var client = new UserServiceGrpc.UserServiceGrpcClient(channel);

            return await client.GetAllAsync(new GetUsersRequest());
        }

        private void Handler(DeliveryReport<string, string> r)
        {
            if (r.Error.IsError)
            {
                throw new Exception(r.Error.Reason);
            }

            _logger.LogInformation($"Delivered message to {r.TopicPartitionOffset}");
        }
    }
}