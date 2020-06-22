using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Account
{
    public class AccountResponse
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public string Agency { get; set; }
        public string AccountNumber { get; set; }
    }
}
