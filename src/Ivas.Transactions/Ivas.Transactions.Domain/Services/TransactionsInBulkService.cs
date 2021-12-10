using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Services;
using Ivas.Transactions.Domain.Contracts.Repositories;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Domain.Validators;
using Ivas.Transactions.Shared.Exceptions.Custom;
using Ivas.Transactions.Shared.Notifications;

namespace Ivas.Transactions.Domain.Services
{
    public interface ITransactionsInBulkService : 
        ICreatableAsyncService<IEnumerable<TransactionCreateDto>>
    {
        
    }
    
    public class TransactionsInBulkService : ITransactionsInBulkService
    {
        private readonly ITransactionRepository _transactionRepository;

        private readonly ITransactionValidator _transactionValidator;
        
        public TransactionsInBulkService(
            ITransactionRepository transactionRepository,
            ITransactionValidator transactionValidator)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _transactionValidator = transactionValidator ?? throw new ArgumentNullException(nameof(transactionValidator));
        }
        
        public async Task<Result> CreateAsync(IEnumerable<TransactionCreateDto> entity)
        {
            var validationResults = _transactionValidator.Validate(entity);
            
            if (validationResults.Any(x => x.IsFailure)) 
                throw new IvasException(
                    string.Join(
                        ", ", 
                        validationResults
                            .Where(x => x.Errors.Any())
                            .Select(x => x.Errors.Select(x => x.Message))));

            var domainEntitiesToInsert = entity
                .Select(transaction => new TransactionCreate(transaction))
                .ToList();

            await _transactionRepository.InsertInBulk(domainEntitiesToInsert);

            return Result.Ok();
        }
    }
}