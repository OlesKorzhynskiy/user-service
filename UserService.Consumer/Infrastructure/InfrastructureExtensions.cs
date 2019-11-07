using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using UserService.Domain;
using UserService.Infrastructure.Config;
using UserService.Infrastructure.EntityConfigurations;
using UserService.Infrastructure.Repositories;
using UserService.Mediator.Extensions;

namespace UserService.Consumer.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

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

        public static IServiceCollection WithMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));

            var pack = new ConventionPack {new IgnoreExtraElementsConvention(true)};
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);

            BaseModelConfiguration.Configure();

            return services;
        }
    }
}