using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Babylon.Investments.Domain.Abstractions.Responses;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Abstractions.Services.Base;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Domain.Strategies;
using Babylon.Investments.Domain.Validators;
using Babylon.Investments.Shared.Exceptions.Custom;
using Babylon.Investments.Shared.Extensions;
using Babylon.Investments.Shared.Notifications;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Domain.Services
{
    public interface ITransactionService :
        ICreatableAsyncService<TransactionPostRequest>, 
        IDeletableAsyncService<TransactionDeleteRequest>
    {
        Task<IEnumerable<TransactionGetResponse>> GetByClientAndUserAsync(string tenantId, string userId);

        Task<TransactionGetResponse> GetSingleAsync(string tenantId, string transactionId);
    }
    
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionValidator _transactionValidator;

        private readonly ITransactionRepository _transactionRepository;

        private readonly IOperationStrategy _operationStrategy;
        
        private readonly IMapper _mapper;

        private readonly ILogger<TransactionService> _logger;

        public TransactionService(
            ITransactionValidator transactionValidator,
            ITransactionRepository transactionRepository,
            IOperationStrategy operationStrategy,
            IMapper mapper,
            ILogger<TransactionService> logger)
        {
            _transactionValidator = transactionValidator 
                                    ?? throw new ArgumentNullException(nameof(transactionValidator));
            
            _transactionRepository = transactionRepository 
                                     ?? throw new ArgumentNullException(nameof(transactionRepository));

            _operationStrategy = operationStrategy ?? throw new ArgumentNullException(nameof(operationStrategy));
            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> CreateAsync(TransactionPostRequest request)
        {
            _logger.LogInformation("Transaction Service - Called method CreateAsync");
            
            var validationResult = _transactionValidator.Validate(request);

            _logger.LogInformation("Validation Result: { Serialize }", JsonSerializer.Serialize(validationResult));
            
            if (validationResult.IsFailure)
            {
                throw new BabylonException(validationResult.Errors.ToFormattedErrorMessage());
            }

            var operationHandler = _operationStrategy.Create(request.TransactionType);

            var domainObject = (TransactionCreate) await operationHandler.HandleAsync(request);

            var transactionResponse = _mapper.Map<TransactionCreate, TransactionPostResponse>(domainObject);
            
            return Result.Ok(transactionResponse);
        }

        public async Task<Result> DeleteAsync(TransactionDeleteRequest entity)
        {
            _logger.LogInformation("Investmentservice - Called method DeleteAsync");
            
            var isEntityValid = _transactionValidator.ValidateDelete(entity);
            
            _logger.LogInformation($"Validation Result: { JsonSerializer.Serialize(isEntityValid) }");
            
            if (isEntityValid.IsFailure)
            {
                throw new BabylonException(
                    string.Join(
                        ", ", 
                        isEntityValid.Errors.Select(x => x.Message))
                    );
            }

            var transactionToDelete =
                await _transactionRepository.GetByIdAsync(entity.TenantId, entity.TransactionId);

            _logger.LogInformation($"Transaction to delete: { JsonSerializer.Serialize(transactionToDelete) }");
            
            if (transactionToDelete == null)
            {
                throw new BabylonException($"Transaction specified does not exist. Please provide a valid transaction");
            }
            
            _logger.LogInformation("Deleting transaction from DynamoDB table..");
            
            await _transactionRepository.Delete(transactionToDelete);

            return Result.Ok(transactionToDelete.TransactionId);
        }

        public async Task<IEnumerable<TransactionGetResponse>> GetByClientAndUserAsync(string tenantId, string userId)
        {
            var clientInvestments =
                (await _transactionRepository.GetAsync(tenantId)).OrderByDescending(x => x.Date);

            var userInvestments = clientInvestments.Where(x => x.UserId.Equals(userId));
            
            return _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionGetResponse>>(userInvestments);
        }

        public async Task<TransactionGetResponse> GetSingleAsync(string tenantId, string transactionId)
        {
            var transactionToGet =
                await _transactionRepository.GetByIdAsync(tenantId, transactionId);

            return _mapper.Map<Transaction, TransactionGetResponse>(transactionToGet);
        }
    }
}