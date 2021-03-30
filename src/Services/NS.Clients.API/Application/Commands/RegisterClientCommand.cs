using FluentValidation;
using NS.Core.Messages;
using System;

namespace NS.Clients.API.Application.Commands
{
    public class RegisterClientCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterClientCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class RegisterClientValidation : AbstractValidator<RegisterClientCommand>
    {
        public RegisterClientValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(x => x.Cpf)
                .Must(CpfIsValid)
                .WithMessage("O CPF informado não é valido!");

            RuleFor(x => x.Email)
                .Must(EmailIsValid)
                .WithMessage("O e-mail informado não é valido!");
        }

        protected static bool EmailIsValid(string email)
        {
            return Core.DomainObjects.Email.Validate(email);
        }

        protected static bool CpfIsValid(string cpf)
        {
            return Core.DomainObjects.Cpf.Validate(cpf);
        }
    }
}
