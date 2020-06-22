using Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test
{
    public class TransferTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AppyTransfer()
        {
            var depositorBalance = 2000;
            var beneficiaryBalance = 1000;
            var amount = 1000;
            Transfer transfer = CreateTrasnfer(amount);
            Account depositor = CreateAccount(depositorBalance);
            Account beneficiary = CreateAccount(beneficiaryBalance);
            transfer.SetDepositor(depositor);
            transfer.SetBeneficiary(beneficiary);
            transfer.Apply();
            Assert.AreEqual(depositor.Balance,  depositorBalance - amount, "Transfer Error on Depositor Account");
            Assert.AreEqual(beneficiary.Balance, beneficiaryBalance + amount, "Transfer Error on Beneficiary Account");
        }
        [Test]
        public void SetDepositor()
        {
            Transfer transfer = CreateTrasnfer(1000);
            Account depositor = CreateAccount(2000);
            transfer.SetDepositor(depositor);
            Assert.AreEqual(transfer.DepositorId, depositor.Id);
        }
        [Test]
        public void SetBeneficiary()
        {
            Transfer transfer = CreateTrasnfer(1000);
            Account beneficiary = CreateAccount(2000);
            transfer.SetBeneficiary(beneficiary);
            Assert.AreEqual(transfer.BeneficiaryId, beneficiary.Id);
        }

        private static Transfer CreateTrasnfer(decimal amount)
        {
            return new Transfer()
            {
                Amount = amount
            };
        }

        private static Account CreateAccount(decimal balance)
        {
            return new Account(balance)
            {
                AccountNumber = "9999",
                Agency = "99999",
                Id = Guid.NewGuid()
            };
        }
    }
}
