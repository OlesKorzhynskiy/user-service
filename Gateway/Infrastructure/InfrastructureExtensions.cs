using System;
using Confluent.Kafka;
using Gateway.UserService.Adapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Serilog;
using UserService.Mediator.Dispatcher;
using UserService.Mediator.Extensions;
using UserService.Query.Client;

namespace Gateway.Infrastructure
{
    public static class InfrastructureExtensions
    {
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

        public static IServiceCollection WithMediator(this IServiceCollection services, IConfiguration configuration)
        {
            var bootstrapServers = configuration.GetSection("MediatorSettings:BootstrapServers").Value;
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };

            services.WithDispatcher(config);

            var dispatcher = services.BuildServiceProvider().GetService<IDispatcher>();
            TransportRoutingConfiguration.RegisterUserServiceRoutes(dispatcher, configuration["Services:UserService:Topic"]);

            return services;
        }

        public static IServiceCollection WithUserService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserServiceAdapter, UserServiceAdapter>();

            services
                .AddRefitClient<IUserServiceWebClient>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(configuration["Services:UserService:QueryUrl"]);
                });

            return services;
        }
    }
}