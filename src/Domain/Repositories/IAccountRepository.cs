using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAsync(string agency, string accountNumber);
        Task<int> UpdateAsync(Account depositor);
        Task<Account> GetAsync(Guid id);
    }
}
