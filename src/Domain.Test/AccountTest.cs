using Domain.Models;
using Domain.Scopes;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Test
{
    public class AccountTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsValidForMakeDeposit()
        {
            Account account = CreateAccount(1000);
            Assert.IsTrue(account.IsValidForMakeDeposit(1).IsSuccess);
        }
        [Test]
        public void NotValidForMakeDeposit()
        {
            Account account = CreateAccount(1000);
            Assert.IsFalse(account.IsValidForMakeDeposit(2000).IsSuccess);
        }
        [Test]
        public void IsValidForReceiveAmount()
        {
            Account account = CreateAccount(1000);
            Assert.IsTrue(account.IsValidForReceiveAmount(1).IsSuccess);
        }
        [Test]
        public void IsValidIncrement()
        {
            Account account = CreateAccount(1000);
            account.IncrementBalance(1000);
            Assert.AreEqual(account.Balance, 2000);
        }
        [Test]
        public void IsValidDecrement()
        {
            Account account = CreateAccount(1000);
            account.DecrementBalance(1000);
            Assert.AreEqual(account.Balance, 0);
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