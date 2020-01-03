using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace UserService.Mediator.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly ProducerConfig _producerConfig;
        private readonly TimeSpan _timeoutTime;
        private static Dictionary<Type, string> _topics;

        static Dispatcher()
        {
            _topics = new Dictionary<Type, string>();
        }

        public Dispatcher(ProducerConfig producerConfig, TimeSpan? timeoutTime = null)
        {
            _producerConfig = producerConfig;
            _timeoutTime = timeoutTime ?? TimeSpan.FromSeconds(10);
        }

        public Task Dispatch<T>(T command, Action<DeliveryReport<string, string>> deliveryHandler = null)
        {
            using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
            {
                var topic = TryGetTopic(typeof(T));

                producer.Produce(topic, new Message<string, string>
                {
                    Key = typeof(T).AssemblyQualifiedName, 
                    Value = JsonConvert.SerializeObject(command)
                }, deliveryHandler);

                producer.Flush(_timeoutTime);
            }

            return Task.CompletedTask;
        }

        public void RegisterRoute(Type type, string route)
        {
            _topics.Add(type, route);
        }

        private string TryGetTopic(Type type)
        {
            try
            {
                return _topics[type];
            }
            catch (Exception)
            {
                throw new SystemException($"Command with type {type} wasn't registered");
            }
        }
    }
}