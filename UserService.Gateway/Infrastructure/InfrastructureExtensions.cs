using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UserService.Query.Client;

namespace UserService.Gateway.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection WithRefit(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRefitClient<IUserServiceWebClient>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(configuration["QueryUrls:UserServiceQueryUrl"]);
                });

            return services;
        }
    }
}