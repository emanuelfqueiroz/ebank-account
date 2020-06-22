using Domain.Common;
using Domain.Models;


namespace Domain.Scopes
{
    public static class AccountScopes
    {
        public static DomainValidation IsValidForMakeDeposit(this Account account, decimal Amount)
        {
            if(account is null)
            {
                return new DomainValidation("Depositor not found");
            }
            if(account.Balance > Amount)
            {
                return DomainValidation.Success;
            }
            return new DomainValidation("Insufficient Balance");
        }
        public static DomainValidation IsValidForReceiveAmount(this Account account, decimal Amount)
        {
            if (account is null)
            {
                return new DomainValidation("Beneficiary not found");
            }
            return DomainValidation.Success;
        }
    }
}
