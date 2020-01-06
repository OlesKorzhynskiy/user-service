using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

                        var method = handler.GetMethod("Handle", new[] {argumentType});
                        var result = method?.Invoke(instance, new object[] {convertedObject}) as Task;
                        
                        HandleExceptions(result);
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                    throw;
                }
            }
        }

        private static void HandleExceptions(Task task)
        {
            if (task == null)
                return;

            try
            {
                task.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}