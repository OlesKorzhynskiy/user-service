using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace UserService.Mediator.Dispatcher
{
    public interface IDispatcher
    {
        Task Dispatch<T>(T command, Action<DeliveryReport<string, string>> deliveryHandler = null);

        void RegisterRoute(Type type, string route);
    }
}