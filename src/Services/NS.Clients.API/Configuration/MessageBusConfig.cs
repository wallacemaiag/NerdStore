using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NS.Clients.API.Services;
using NS.Core.Tools;
using NS.MessageBus;

namespace NS.Clients.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegisterClientIntegrationHandler>();
        }
    }
}
