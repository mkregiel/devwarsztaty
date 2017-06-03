using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using devwarsztaty.service.Framework;
using RawRabbit.vNext;
using devwarsztaty.service.Handlers;
using devwarsztaty.messages.Commands;
using RawRabbit;

namespace devwarsztaty.service
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            ConfigureRabbitMq(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            ConfigureHandlers(app);

            app.UseMvc();

            
        }

        private void ConfigureRabbitMq(IServiceCollection serviceCollection)
        {
            var options = new RabbitMqOptions();
            var section = Configuration.GetSection("rabbitmq");

            section.Bind(options);

            serviceCollection.Configure<RabbitMqOptions>(section);

            var client = BusClientFactory.CreateDefault(options);

            serviceCollection.AddSingleton<IBusClient>(client);

            serviceCollection.AddScoped<ICommandHandler<CreateRecord>,CreateRecordHandler>();
        }

        private void ConfigureHandlers(IApplicationBuilder app)
        {
            var client = app.ApplicationServices.GetService<IBusClient>();
            client.SubscribeAsync<CreateRecord>((msq, ctx) =>
                app.ApplicationServices.GetService<ICommandHandler<CreateRecord>>().HandleAsync(msq)
            );
        }
    }
}
