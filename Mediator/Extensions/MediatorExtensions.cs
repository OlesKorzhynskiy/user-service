using System.Threading;
using Confluent.Kafka;
using Mediator.Dispatcher;
using Mediator.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Extensions
{
    public static class MediatorExtensions
    {
        public static IServiceCollection WithDispatcher(this IServiceCollection services, ProducerConfig config, string defaultTopic)
        {
            services.AddTransient<IDispatcher>(sp => new Dispatcher.Dispatcher(config, defaultTopic));

            return services;
        }

        public static IServiceCollection WithConsumer(this IServiceCollection services, ConsumerConfig config, string topic)
        {
            var messageHandler = new Thread(() => { HandlerService.HandleMessages(config, topic); });
            messageHandler.Start();

            return services;
        }
    }
}