using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NS.Clients.API.Application.Commands;
using NS.Core.MediatR;
using NS.Core.Messages.Integration;
using NS.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NS.Clients.API.Services
{
    public class RegisterClientIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterClientIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<RegistredUserIntegrationEvent, ResponseMessage>(async request =>
            await RegisterClient(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();

            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegisterClient(RegistredUserIntegrationEvent message)
        {
            var clientCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult success;
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatrHandler>();
                success = await mediator.SendCommand(clientCommand);
            }

            return new ResponseMessage(success);
        }
    }
}
