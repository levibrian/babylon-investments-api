using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Babylon.Investments.Domain.Abstractions.Responses;
using Babylon.Networking.Interfaces.Brokers;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Abstractions.Services.Base;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Domain.Services.Base;
using Babylon.Investments.Domain.Validators;
using Babylon.Investments.Shared.Exceptions.Custom;
using Babylon.Investments.Shared.Notifications;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Domain.Services
{
    public interface ITransactionService :
        ICreatableAsyncService<TransactionPostRequest>, 
        IDeletableAsyncService<TransactionDeleteRequest>
    {
        Task<IEnumerable<TransactionGetResponse>> GetByClientAndUserAsync(string clientIdentifier, string userId);

        Task<TransactionGetResponse> GetSingleAsync(string clientIdentifier, string transactionId);
    }
    
    public class TransactionService : TransactionBaseService, ITransactionService
    {
        private readonly ITransactionValidator _transactionValidator;

        private readonly ITransactionRepository _transactionRepository;
        
        private readonly IMapper _mapper;

        private readonly ILogger<TransactionService> _logger;

        public TransactionService(
            ITransactionValidator transactionValidator,
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ILogger<TransactionService> logger) : base (transactionValidator, transactionRepository, mapper, logger)
        {
            _transactionValidator = transactionValidator 
                                    ?? throw new ArgumentNullException(nameof(transactionValidator));
            
            _transactionRepository = transactionRepository 
                                     ?? throw new ArgumentNullException(nameof(transactionRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public new async Task<Result> CreateAsync(TransactionPostRequest request) => await base.CreateAsync(request);

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
                await _transactionRepository.GetByIdAsync(entity.ClientIdentifier, entity.TransactionId);

            _logger.LogInformation($"Transaction to delete: { JsonSerializer.Serialize(transactionToDelete) }");
            
            if (transactionToDelete == null)
            {
                throw new BabylonException($"Transaction specified does not exist. Please provide a valid transaction");
            }
            
            _logger.LogInformation("Deleting transaction from DynamoDB table..");
            
            await _transactionRepository.Delete(transactionToDelete);

            return Result.Ok(transactionToDelete.TransactionId);
        }

        public async Task<IEnumerable<TransactionGetResponse>> GetByClientAndUserAsync(string clientIdentifier, string userId)
        {
            var clientInvestments =
                (await _transactionRepository.GetByClientAsync(clientIdentifier)).OrderByDescending(x => x.Date);

            var userInvestments = clientInvestments.Where(x => x.UserId.Equals(userId));
            
            return _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionGetResponse>>(userInvestments);
        }

        public async Task<TransactionGetResponse> GetSingleAsync(string clientIdentifier, string transactionId)
        {
            var transactionToGet =
                await _transactionRepository.GetByIdAsync(clientIdentifier, transactionId);

            return _mapper.Map<Transaction, TransactionGetResponse>(transactionToGet);
        }
    }
}