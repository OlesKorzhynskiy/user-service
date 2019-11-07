using System;
using System.Linq;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UserService.Mediator.Extensions;

namespace UserService.Mediator.Handler
{
    public static class HandlerService
    {
        public static void HandleMessages(IServiceCollection services, ConsumerConfig consumerConfig, string topic)
        {
            var type = typeof(IHandleMessages<>);
            var allHandlers = type.GetAssignableTypes().ToList();

            using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();

            consumer.Subscribe(topic);

            try
            {
                while (true)
                {
                    try
                    {
                        var message = consumer.Consume();

                        var argumentType = Type.GetType(message.Key);
                        dynamic convertedObject = JsonConvert.DeserializeObject(message.Value, argumentType);

                        var handlers = allHandlers.GetTypesImplementingInterfaceWithSpecificArgument(argumentType);
                        foreach (var handler in handlers)
                        {
                            var provider = services.BuildServiceProvider();
                            var instance = ActivatorUtilities.CreateInstance(provider, handler);
                            var method = handler.GetMethod("Handle", new [] {argumentType});
                            method?.Invoke(instance, new object[] { convertedObject });
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}