using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.UserAggregate;
using UserService.Infrastructure.Repositories;
using UserService.Mediator.Extensions;

namespace UserService.Consumer.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection WithMediator(this IServiceCollection services, IConfiguration configuration)
        {
            var mediatorSection = configuration.GetSection("MediatorSettings");
            var topic = mediatorSection.GetSection("Topic").Value;
            var config = new ConsumerConfig();
            mediatorSection.Bind(config);

            services.WithConsumer(config, topic);
            
            return services;
        }
    }
}