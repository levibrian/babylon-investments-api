using System;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Domain.Validators;
using Babylon.Investments.Shared.Extensions;

namespace Babylon.Investments.Domain.Handlers
{
    public class SellOperationHandler : IOperationHandler   
    {
        private readonly ITransactionRepository _transactionRepository;

        private readonly IOperationValidator _operationValidator;

        public SellOperationHandler(
            ITransactionRepository transactionRepository, 
            IOperationValidator operationValidator)
        {
            _transactionRepository = transactionRepository;
            _operationValidator = operationValidator;
        }

        public async Task<Transaction> HandleAsync(TransactionPostRequest request)
        {
            var companyTransactionHistory = 
                await _transactionRepository.GetByTickerAsync(request.TenantId, request.Ticker);
            
            // Missing logic to validate if the tenant has Transactions for that  Ticker.

            var validationResult = _operationValidator.Validate(companyTransactionHistory);

            if (validationResult.IsFailure)
            {
                throw new InvalidOperationException(validationResult.Errors.ToFormattedErrorMessage());
            }
            
            var domainObject = new TransactionCreate(request, companyTransactionHistory);
            
            await _transactionRepository.Insert(domainObject);

            return domainObject;
        }
    }
}