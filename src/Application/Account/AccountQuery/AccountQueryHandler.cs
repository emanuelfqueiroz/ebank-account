using Application.Account.Mappers;
using CQRSHelper._Common;
using Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.AccountQuery
{
    public class AccountQueryHandler : IQueryHandler<AccountQuery, AccountResponse>
    {
        public IAccountRepository _repo { get; set; }

        public AccountQueryHandler(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<AccountResponse> Handle(AccountQuery req, CancellationToken cancellationToken)
        {
            var account = await _repo.GetAsync(agency: req.Agency, accountNumber: req.AccountNumber);
            return account.Map();
        }
    }
}
