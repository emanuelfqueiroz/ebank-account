using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public decimal Balance { get; private set; }
        public string Agency { get; set; }
        public string AccountNumber { get; set; }
        
        public void DecrementBalance(decimal decrement)
        {
            this.Balance -= decrement;
        }

        public void IncrementBalance(decimal increment)
        {
            this.Balance += increment;
        }
        public Account() {
            Id = Guid.NewGuid();
        }
        public Account(decimal balance) : this()
        {
            Balance = balance;
        }
    }
}
