using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Dtos;
using Babylon.Networking.Interfaces.Brokers;
using Babylon.Investments.Domain.Abstractions.Services;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Services.Base;
using Babylon.Investments.Domain.Validators;
using Babylon.Investments.Shared.Exceptions.Custom;
using Babylon.Investments.Shared.Notifications;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Domain.Services
{
    public interface ITransactionsInBulkService : 
        ICreatableAsyncService<IEnumerable<TransactionPostDto>>,
        IDeletableAsyncService<IEnumerable<TransactionDeleteDto>>
    {
    }
    
    public class TransactionsInBulkService : TransactionBaseService, ITransactionsInBulkService
    {
        private readonly ITransactionRepository _transactionRepository;

        private readonly ITransactionValidator _transactionValidator;

        private readonly IFinancialsBroker _financialsBroker;
        
        private readonly ILogger<TransactionsInBulkService> _logger;
        
        public TransactionsInBulkService(
            ITransactionRepository transactionRepository,
            ITransactionValidator transactionValidator,
            IFinancialsBroker financialsBroker,
            ILogger<TransactionsInBulkService> logger) : base(transactionValidator, transactionRepository, financialsBroker, logger)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _transactionValidator = transactionValidator ?? throw new ArgumentNullException(nameof(transactionValidator));
            _financialsBroker = financialsBroker ?? throw new ArgumentNullException(nameof(financialsBroker));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<Result> CreateAsync(IEnumerable<TransactionPostDto> entity)
        {
            foreach (var transactionToCreate in entity)
            {
                await base.CreateAsync(transactionToCreate);
            }

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(IEnumerable<TransactionDeleteDto> transactionsToDelete)
        {
            var validationResults =
                _transactionValidator
                    .ValidateDelete(transactionsToDelete)
                    .ToList();
            
            if (validationResults.Any(x => x.IsFailure)) 
                throw new BabylonException(
                    string.Join(
                        ", ", 
                        validationResults
                            .Where(x => x.Errors.Any())
                            .SelectMany(x => x.Errors.Select(x => x.Message))));

            var entitiesToDelete = new List<Transaction>();
            
            foreach (var transaction in transactionsToDelete)
            {
                var fetchedTransactionFromDb =
                    await _transactionRepository.GetByIdAsync(transaction.ClientIdentifier, transaction.TransactionId);
                
                if (fetchedTransactionFromDb == null) 
                    _logger.LogWarning($"InvestmentsInBulkService - Delete In Bulk...Transaction with ClientIdentifier: {transaction.ClientIdentifier} and with TransactionId: {transaction.TransactionId} not found..");
                else 
                    entitiesToDelete.Add(fetchedTransactionFromDb);
            }

            await _transactionRepository.DeleteInBulk(entitiesToDelete);
            
            return Result.Ok();
        }
    }
}