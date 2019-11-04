using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Consumer.Infrastructure;
using Contracts.Contracts;
using Newtonsoft.Json;

namespace Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Bootstrapper.RunAsync(args);
        }
    }
}
