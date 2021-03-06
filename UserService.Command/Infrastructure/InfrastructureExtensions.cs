﻿using AutoMapper;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using Serilog;
using UserService.Domain;
using UserService.Infrastructure.Config;
using UserService.Infrastructure.EntityConfigurations;
using UserService.Infrastructure.Repositories;
using UserService.Mediator.Extensions;

namespace UserService.Command.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }

        public static IServiceCollection WithAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile).Assembly);

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

        public static IServiceCollection WithLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss.fff}] [{Level:u3}] [{ThreadId}] [{SourceContext}]{Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            services.AddLogging(c => c.AddSerilog(Log.Logger));

            return services;
        }
    }
}