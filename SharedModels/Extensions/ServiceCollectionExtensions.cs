using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedModels.OptionModels;
using SharedModels.Services;
using System.Reflection;

namespace SharedModels.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static IHostApplicationBuilder AddMessageBrokerService(this IHostApplicationBuilder builder)
        {
            var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Uri, h =>
                    {
                        h.Username(rabbitMqSettings.Username);
                        h.Password(rabbitMqSettings.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });

                var entryAssembly = Assembly.GetEntryAssembly();

                x.AddConsumers(entryAssembly);
            });

            builder.Services.AddSingleton<IMessageBus, MessageBus>();

            return builder;
        }
    }
}
