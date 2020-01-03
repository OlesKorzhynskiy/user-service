using System.Threading;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using UserService.Mediator.Dispatcher;
using UserService.Mediator.Handler;

namespace UserService.Mediator.Extensions
{
    public static class MediatorExtensions
    {
        public static IServiceCollection WithDispatcher(this IServiceCollection services, ProducerConfig config)
        {
            services.AddTransient<IDispatcher>(sp => new Dispatcher.Dispatcher(config));

            return services;
        }

        public static IServiceCollection WithConsumer(this IServiceCollection services, ConsumerConfig config, string topic)
        {
            var messageHandler = new Thread(() => { HandlerService.HandleMessages(services, config, topic); });
            messageHandler.Start();

            return services;
        }
    }
}