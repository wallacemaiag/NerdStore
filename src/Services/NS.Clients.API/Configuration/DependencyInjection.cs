using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NS.Clients.API.Application.Commands;
using NS.Clients.API.Application.Events;
using NS.Clients.API.Data;
using NS.Clients.API.Data.Repository;
using NS.Clients.API.Models;
using NS.Core.MediatR;

namespace NS.Clients.API.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatrHandler, MediatrHandler>();
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<INotificationHandler<RegistredCustomerEvent>, ClientEventHandler>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ClientsContext>();
        }
    }
}
