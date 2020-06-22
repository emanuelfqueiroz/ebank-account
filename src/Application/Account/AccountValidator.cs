using FluentValidation;

namespace Application.Account
{
    public class AccountValidator : AbstractValidator<AccountDTO>
    {
        public AccountValidator()
        {
            RuleFor(b => b.AccountNumber).NotNull().NotEmpty();
            RuleFor(b => b.Agency).NotNull().NotEmpty();
            //RuleFor CheckDigit
        }
    }

}
