using Application.TransferCommands.Transfer;
using CQRSHelper._Common;
using Domain.Common;
using Domain.Models;
using Domain.Repositories;
using Domain.Scopes;
using FluentValidation.Results;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Transfer.Commands
{
    public class TrasnferCommandHandler : ICommandHandler<TransferCommand, TrasnferCompletedResponse>
    {
        IAccountRepository _accountRepo;
        AppDbContext _unitOfWork;

        public TrasnferCommandHandler(IAccountRepository accountRepo, AppDbContext unitOfWork)
        {
            _accountRepo = accountRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<TrasnferCompletedResponse> Handle(TransferCommand cmd, CancellationToken cancelToken)
        {
            var response = new TrasnferCompletedResponse() { Status = false };
            ValidationResult validation = Validate(cmd);
            if (!validation.IsValid)
            {
                response.Error(validation.Errors.First().ErrorMessage);
                return response;
            }

            //check for lock
            //Lock depositor and beneficiary
            DomainValidation dValidation = DomainValidation.Success;
            var transfer = await LoadAsync(cmd, dValidation);
            if (!dValidation.IsSuccess)
            {
                response.Error(dValidation.Message);
                return response;
            }

            if (transfer != null)
            {
                transfer.Apply();

                SetUnitOfWork(transfer);
                await _unitOfWork.SaveChangesAsync();

                SetResponse(response, transfer);

                return response;
            }
            //unlock depositor and beneficiary : on Dispose Handler or try-finally or with using 
            return response;
        }

        
        public ValidationResult Validate(TransferCommand cmd)
        {
            return new TrasnferCommandValidator().Validate(cmd);
        }

        /// <summary>
        /// Safe Loading 
        /// Check Beneficiary before load Depositor
        /// </summary>
        /// <returns>Transfer</returns>
        private async Task<Domain.Models.Transfer> LoadAsync(TransferCommand cmd, DomainValidation vld)
        {
            var transfer = new Domain.Models.Transfer();

            var beneficiary = await _accountRepo.GetAsync(
                agency: cmd.Beneficiary.Agency,
                accountNumber: cmd.Beneficiary.AccountNumber);

            vld = beneficiary.IsValidForReceiveAmount(cmd.Amount);
            
            if (!vld.IsSuccess)
                return null;

            transfer.SetBeneficiary(beneficiary);

            var depositor = await _accountRepo.GetAsync(
                agency: cmd.Depositor.Agency,
                accountNumber: cmd.Depositor.AccountNumber);

            vld = depositor.IsValidForMakeDeposit(cmd.Amount);
            if (!vld.IsSuccess)
                return null;

            transfer.SetDepositor(depositor);
            return transfer;
        }

        private void SetUnitOfWork(Domain.Models.Transfer transfer)
        {
            _unitOfWork.Trasnfers.Add(transfer);
            _unitOfWork.Accounts.Attach(transfer.Depositor).State = EntityState.Modified;
            _unitOfWork.Accounts.Attach(transfer.Beneficiary).State = EntityState.Modified;
        }
        private void SetResponse(TrasnferCompletedResponse res, Domain.Models.Transfer transfer)
        {
            res.Message = transfer.Status ? "Transfer was completed successfully" : "Failure";
            res.TrasnferNumber = transfer.TrasnferNumber;
            res.CurrentBalance = transfer.Depositor.Balance;
            res.Status = transfer.Status;
        }
    }
}
