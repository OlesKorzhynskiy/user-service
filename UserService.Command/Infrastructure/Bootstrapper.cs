using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UserService.Command.Infrastructure
{
    public class Bootstrapper
    {
        private const string Appsettings = "appsettings.json";
        private const string Hostsettings = "hostsettings.json";

        public static Task RunAsync(string[] args)
        {
            return new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile(Hostsettings, true);
                    configHost.AddEnvironmentVariables();
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile(Appsettings, true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true);
                    configApp.AddEnvironmentVariables();
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .WithLogger()
                        .WithServices()
                        .WithMongo(hostContext.Configuration)
                        .WithMediator(hostContext.Configuration);

                    services.BuildServiceProvider();
                })
                .UseConsoleLifetime()
                .Build()
                .RunAsync();
        }
    }
}