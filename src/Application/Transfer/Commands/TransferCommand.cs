using Application.Account;
using Application.TransferCommands.Transfer;
using CQRSHelper._Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Transfer.Commands
{
    public class TransferCommand : CommandBase<TrasnferCompletedResponse>
    {
        public AccountDTO Depositor { get; set; }
        public AccountDTO Beneficiary { get; set; }
        public decimal Amount { get; set; }
    }
}
