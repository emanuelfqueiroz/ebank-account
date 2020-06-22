using Application.Transfer.Commands;
using Domain.Repositories;
using InfraStructure;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Test
{
    public class TrasnferCommandTest
    {
        public TransferCommand cmd { get; private set; }
        public TrasnferCommandHandler handler { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.handler = new TrasnferCommandHandler(new Mock<IAccountRepository>().Object, null);
            this.cmd = new TransferCommand()
            {
                Amount = 1000,
                Beneficiary = new Account.AccountDTO
                {
                    AccountNumber = "1234",
                    Agency = "9999"
                },
                Depositor = new Account.AccountDTO
                {
                    Agency = "9999",
                    AccountNumber = "9999"
                }
            };
        }

      
        [Test]
        public void ValidCommand()
        {
            Assert.IsTrue(handler.Validate(cmd).IsValid);
        }
        [Test]
        public void NotValidAgency()
        {
            cmd.Beneficiary.Agency = String.Empty;
            Assert.IsFalse(handler.Validate(cmd).IsValid);
        }

        [Test]
        public void NullBeneficiary()
        {
            cmd.Beneficiary = null;
            Assert.IsFalse(handler.Validate(cmd).IsValid);
        }
        [Test]
        public void NullDepositor()
        {
            cmd.Depositor = null;
            Assert.IsFalse(handler.Validate(cmd).IsValid);
        }
        [Test]
        public void NotValidAmout()
        {
            cmd.Amount = -1;
            Assert.IsFalse(handler.Validate(cmd).IsValid);
        }


    }
}