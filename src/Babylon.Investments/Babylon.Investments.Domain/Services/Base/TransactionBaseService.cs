using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Dtos;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Validators;
using Babylon.Investments.Shared.Exceptions.Custom;
using Babylon.Investments.Shared.Notifications;
using Babylon.Networking.Interfaces.Brokers;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Domain.Services.Base
{
    public abstract class TransactionBaseService
    {
        private readonly ITransactionValidator _transactionValidator;

        private readonly ITransactionRepository _transactionRepository;

        private readonly IFinancialsBroker _financialsBroker;
        
        private readonly ILogger<TransactionBaseService> _logger;

        protected TransactionBaseService(
            ITransactionValidator transactionValidator,
            ITransactionRepository transactionRepository,
            IFinancialsBroker financialsBroker,
            ILogger<TransactionBaseService> logger)
        {
            _transactionValidator = transactionValidator ?? throw new ArgumentNullException(nameof(transactionValidator));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _financialsBroker = financialsBroker ?? throw new ArgumentNullException(nameof(financialsBroker));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task<Result> CreateAsync(TransactionPostDto dto)
        {
            _logger.LogInformation("Transaction Service - Called method CreateAsync");
            
            var validationResult = _transactionValidator.Validate(dto);

            _logger.LogInformation($"Validation Result: { JsonSerializer.Serialize(validationResult) }");
            
            if (validationResult.IsFailure)
            {
                throw new BabylonException(
                    string.Join(", ", validationResult.Errors.Select(x => x.Message)));
            }

            var companyTransactionHistory = (await _transactionRepository
                    .GetByClientAsync(dto.ClientIdentifier))
                .Where(t => t.Ticker.Equals(dto.Ticker));
            
            var domainObject = new TransactionCreate(dto, companyTransactionHistory, _financialsBroker);

            await _transactionRepository.Insert(domainObject);
            
            return Result.Ok(domainObject.TransactionId);
        }
    }
}