using System;
using System.IO;
using AutoMapper;
using Gateway.Infrastructure;
using Gateway.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users API", Version = "v1" });

                var apiGatewayXmlPath = Path.Combine(AppContext.BaseDirectory, "Gateway.xml");
                var contractsXmlPath = Path.Combine(AppContext.BaseDirectory, "Gateway.Contracts.xml");

                if (File.Exists(apiGatewayXmlPath))
                {
                    c.IncludeXmlComments(apiGatewayXmlPath, true);
                }

                if (File.Exists(contractsXmlPath))
                {
                    c.IncludeXmlComments(contractsXmlPath);
                }
            });

            services.AddAutoMapper(typeof(MapperProfile).Assembly);

            services
                .WithLogger()
                .WithUserService(Configuration)
                .WithMediator(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.RoutePrefix = string.Empty;
            });

            app.UseExceptionMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
