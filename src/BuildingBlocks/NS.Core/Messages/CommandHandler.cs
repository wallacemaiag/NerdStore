using FluentValidation.Results;
using NS.Core.Data;
using System.Threading.Tasks;

namespace NS.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnityOfWork uow)
        {
            if (!await uow.Commit()) AddError("Houve um erro ao persistir os dados");

            return ValidationResult;
        }
    }
}
