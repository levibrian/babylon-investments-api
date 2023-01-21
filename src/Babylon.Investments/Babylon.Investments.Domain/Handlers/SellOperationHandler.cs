using System;
using System.Linq;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;
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

        public async Task<BuyOperation> HandleAsync(TransactionPostRequest request)
        {
            var companyTransactionHistory = 
                (await _transactionRepository.GetByTickerAsync(request.TenantId, request.Ticker))
                .ToList();
            
            var validationResult = _operationValidator.Validate(request, companyTransactionHistory);

            if (!validationResult.IsSuccess)
            {
                throw new InvalidOperationException(validationResult.Errors.ToFormattedResponseErrorMessage());
            }
            
            var domainObject = new BuyOperation(request, companyTransactionHistory);
            
            await _transactionRepository.Insert(domainObject);

            return domainObject;
        }
    }
}