using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<Account> GetAsync(string agency, string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(
                a => a.Agency == agency && a.AccountNumber == accountNumber);
        }

        public async Task<Account> GetAsync(Guid id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(id));
        }

        public async Task<int> UpdateAsync(Account depositor)
        {
            _context.Accounts.Attach(depositor).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
