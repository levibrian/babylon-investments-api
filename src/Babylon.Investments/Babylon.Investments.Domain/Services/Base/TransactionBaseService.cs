using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Abstractions.Responses;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Validators;
using Babylon.Investments.Shared.Exceptions.Custom;
using Babylon.Investments.Shared.Notifications;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Domain.Services.Base
{
    public abstract class TransactionBaseService
    {
        private readonly ITransactionValidator _transactionValidator;

        private readonly ITransactionRepository _transactionRepository;

        private readonly IMapper _mapper;
        
        private readonly ILogger<TransactionBaseService> _logger;

        protected TransactionBaseService(
            ITransactionValidator transactionValidator,
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ILogger<TransactionBaseService> logger)
        {
            _transactionValidator = transactionValidator ?? throw new ArgumentNullException(nameof(transactionValidator));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task<Result> CreateAsync(TransactionPostRequest request)
        {
            _logger.LogInformation("Transaction Service - Called method CreateAsync");
            
            var validationResult = _transactionValidator.Validate(request);

            _logger.LogInformation($"Validation Result: { JsonSerializer.Serialize(validationResult) }");
            
            if (validationResult.IsFailure)
            {
                throw new BabylonException(
                    string.Join(", ", validationResult.Errors.Select(x => x.Message)));
            }

            var companyTransactionHistory = (await _transactionRepository
                    .GetByTenantAsync(request.TenantId))
                .Where(t => t.Ticker.Equals(request.Ticker.ToUpperInvariant()))
                .ToList();
            
            var domainObject = new TransactionCreate(request, companyTransactionHistory);

            await _transactionRepository.Insert(domainObject);

            var transactionResponse = _mapper.Map<TransactionCreate, TransactionPostResponse>(domainObject);
            
            return Result.Ok(transactionResponse);
        }
    }
}