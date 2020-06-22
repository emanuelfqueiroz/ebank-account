using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repositories
{
    public class TrasnferRepository : ITransferRepository
    {

        AppDbContext _context;

        public TrasnferRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task Add(Transfer t)
        {
            throw new NotImplementedException();
        }

    }
}
