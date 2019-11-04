using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Mediator.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly ProducerConfig _producerConfig;
        private readonly TimeSpan _timeoutTime;
        private string _topic;

        public Dispatcher(ProducerConfig producerConfig, string topic, TimeSpan? timeoutTime = null)
        {
            _producerConfig = producerConfig;
            _timeoutTime = timeoutTime ?? TimeSpan.FromSeconds(10);
            _topic = topic;
        }

        public Task Dispatch<T>(T command, Action<DeliveryReport<string, string>> deliveryHandler = null)
        {
            using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
            {
                producer.Produce(_topic, new Message<string, string>
                {
                    Key = typeof(T).AssemblyQualifiedName, 
                    Value = JsonConvert.SerializeObject(command)
                }, deliveryHandler);
                producer.Flush(_timeoutTime);
            }

            return Task.CompletedTask;
        }

        public void SetTopic(string value) => _topic = value;
    }
}