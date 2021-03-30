using FluentValidation.Results;
using MediatR;
using NS.Clients.API.Application.Events;
using NS.Clients.API.Models;
using NS.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace NS.Clients.API.Application.Commands
{
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
    {
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Client(message.Id, message.Name, message.Email, message.Cpf);

            var exists = await _clientRepository.GetByCpf(client.Cpf.Number);

            if (exists != null)
            {
                AddError("Este CPF já esta em uso no nosso sistema, caso não foi você que o cadastrou entrar em contato com o nosso time de atendimento");
                return ValidationResult;
            }

            _clientRepository.Add(client);

            client.AddEvent(new RegistredCustomerEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await PersistData(_clientRepository.unityOfWork);
        }
    }
}
