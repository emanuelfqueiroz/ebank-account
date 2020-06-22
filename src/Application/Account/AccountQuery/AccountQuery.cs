using CQRSHelper._Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Account.AccountQuery
{
    public class AccountQuery : IQuery<AccountResponse>
    {
        [Required]
        public string Agency { get; set; }
        [Required]
        public string AccountNumber { get; set; }
    }
}
