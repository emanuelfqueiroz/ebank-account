using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Transfer
    {
        [Key]
        public Guid EntryId { get; set; }
        public string TrasnferNumber => EntryId.ToString();
        
        public Guid  DepositorId { get; set; }
        public virtual Account Depositor { get; private set; }
        public Guid BeneficiaryId { get; set; }
        public virtual Account Beneficiary { get; private set; }

        public decimal Amount { get; set; }
        public bool Status { get; private set; } = false;
        public DateTime CreatedAt { get; set; }


        public void Apply()
        {
            Depositor.DecrementBalance(Amount);
            Beneficiary.IncrementBalance(Amount);
            Status = true;
        }

        public void SetDepositor(Account depositor)
        {
            this.Depositor = depositor;
            this.DepositorId = depositor.Id;
        }
        public void SetBeneficiary(Account beneficiary)
        {
            this.Beneficiary = beneficiary;
            this.BeneficiaryId = beneficiary.Id;
        }

        public Transfer()
        {
            EntryId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
