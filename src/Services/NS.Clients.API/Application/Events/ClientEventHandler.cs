using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace NS.Clients.API.Application.Events
{
    public class ClientEventHandler : INotificationHandler<RegistredCustomerEvent>
    {
        public Task Handle(RegistredCustomerEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
