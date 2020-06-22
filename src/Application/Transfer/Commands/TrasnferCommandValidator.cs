using Application.Account;
using Application.Transfer.Commands;
using FluentValidation;

namespace Application.TransferCommands.Transfer
{
    internal class TrasnferCommandValidator : AbstractValidator<TransferCommand>
    {
        public TrasnferCommandValidator()
        {
            RuleFor(t => t.Amount).GreaterThan(0);
            RuleFor(t => t.Depositor).NotNull().SetValidator(new AccountValidator());
            RuleFor(t => t.Beneficiary).NotNull().SetValidator(new AccountValidator());
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }

}
