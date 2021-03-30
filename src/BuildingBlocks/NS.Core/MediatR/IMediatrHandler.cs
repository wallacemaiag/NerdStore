using FluentValidation.Results;
using NS.Core.Messages;
using System.Threading.Tasks;

namespace NS.Core.MediatR
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
