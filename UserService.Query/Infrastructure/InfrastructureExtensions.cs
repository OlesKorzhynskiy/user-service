using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using Serilog;
using UserService.Domain;
using UserService.Infrastructure.Config;
using UserService.Infrastructure.EntityConfigurations;
using UserService.Infrastructure.Repositories;
using UserService.Query.Services.Interfaces;

namespace UserService.Query.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IUserService), typeof(Services.Concrete.UserService));

            return services;
        }

        public static IServiceCollection WithMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));

            var pack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);

            BaseModelConfiguration.Configure();

            return services;
        }

        public static IServiceCollection WithLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss.fff}] [{Level:u3}] [{ThreadId}] [{SourceContext}]{Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            services.AddLogging(c => c.AddSerilog(Log.Logger));

            return services;
        }
    }
}