using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using UserService.Command.Contracts;
using UserService.Mediator.Dispatcher;
using UserService.Query.Client;
using UserService.Query.Contracts;

namespace Gateway.UserService.Adapter
{
    public class UserServiceAdapter : IUserServiceAdapter
    {
        private readonly IDispatcher _dispatcher;
        private readonly IUserServiceWebClient _userServiceWebClient;
        private readonly ILogger<UserServiceAdapter> _logger;

        public UserServiceAdapter(IDispatcher dispatcher, IUserServiceWebClient userServiceWebClient, ILogger<UserServiceAdapter> logger)
        {
            _dispatcher = dispatcher;
            _userServiceWebClient = userServiceWebClient;
            _logger = logger;
        }

        public async Task Create(CreateUser command)
        {
            await _dispatcher.Dispatch(command, Handler);
        }

        public async Task Update(UpdateUser command)
        {
            await _dispatcher.Dispatch(command, Handler);
        }

        public Task<IEnumerable<UserReadModel>> GetAll()
        {
            return _userServiceWebClient.GetAll();
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