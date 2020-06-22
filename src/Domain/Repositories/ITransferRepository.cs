using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITransferRepository
    {
        Task Add(Transfer t);
    }
}
