using System.Threading.Tasks;
using UserService.Consumer.Infrastructure;

namespace UserService.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Bootstrapper.RunAsync(args);
        }
    }
}
