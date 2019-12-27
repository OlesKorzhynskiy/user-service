using System.Threading.Tasks;
using UserService.Command.Infrastructure;

namespace UserService.Command
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Bootstrapper.RunAsync(args);
        }
    }
}
