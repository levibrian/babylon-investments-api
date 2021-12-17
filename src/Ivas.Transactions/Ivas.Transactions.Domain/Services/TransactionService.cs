﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Ivas.Transactions.Domain.Abstractions.Services;
using Ivas.Transactions.Domain.Contracts.Repositories;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Domain.Requests;
using Ivas.Transactions.Domain.Validators;
using Ivas.Transactions.Shared.Exceptions.Custom;
using Ivas.Transactions.Shared.Notifications;
using Microsoft.Extensions.Logging;

namespace Ivas.Transactions.Domain.Services
{
    public interface ITransactionService :
        ICreatableAsyncService<TransactionSubmitDto>, 
        IDeletableAsyncService<TransactionSubmitDto>
    {
        Task<IEnumerable<TransactionDto>> GetByClientAndUserAsync(string clientIdentifier, string userId);

        Task<TransactionDto> GetSingleAsync(TransactionBaseRequest transactionRequest);
    }
    
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionValidator _transactionValidator;

        private readonly ITransactionRepository _transactionRepository;

        private readonly IMapper _mapper;

        private readonly ILogger<TransactionService> _logger;

        public TransactionService(
            ITransactionValidator transactionValidator,
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ILogger<TransactionService> logger)
        {
            _transactionValidator = transactionValidator 
                                    ?? throw new ArgumentNullException(nameof(transactionValidator));
            
            _transactionRepository = transactionRepository 
                                     ?? throw new ArgumentNullException(nameof(transactionRepository));
            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> CreateAsync(TransactionSubmitDto dto)
        {
            _logger.LogInformation("TransactionService - Called method CreateAsync");
            
            var validationResult = _transactionValidator.Validate(dto);

            _logger.LogInformation($"Validation Result: { JsonSerializer.Serialize(validationResult) }");
            
            if (validationResult.IsFailure)
            {
                throw new IvasException(string.Join(", ", validationResult.Errors.Select(x => x.Message)));
            }
            
            var domainObject = new TransactionCreate(dto);

            await _transactionRepository.Insert(domainObject);
            
            return Result.Ok(domainObject.TransactionId);
        }

        public async Task<Result> DeleteAsync(TransactionSubmitDto entity)
        {
            _logger.LogInformation("TransactionService - Called method DeleteAsync");
            
            var isEntityValid = _transactionValidator.ValidateDelete(entity);
            
            _logger.LogInformation($"Validation Result: { JsonSerializer.Serialize(isEntityValid) }");
            
            if (isEntityValid.IsFailure)
            {
                throw new IvasException(
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
                throw new IvasException($"Transaction specified does not exist. Please provide a valid transaction");
            }
            
            _logger.LogInformation("Deleting transaction from DynamoDB table..");
            
            await _transactionRepository.Delete(transactionToDelete);

            return Result.Ok(transactionToDelete.TransactionId);
        }

        public async Task<IEnumerable<TransactionDto>> GetByClientAndUserAsync(string clientIdentifier, string userId)
        {
            var clientTransactions =
                (await _transactionRepository.GetByClientAsync(clientIdentifier)).OrderByDescending(x => x.Date);

            var userTransactions = clientTransactions.Where(x => x.UserId.Equals(userId));
            
            return _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionDto>>(userTransactions);
        }

        public async Task<TransactionDto> GetSingleAsync(TransactionBaseRequest transactionRequest)
        {
            var transactionToGet =
                await _transactionRepository.GetByIdAsync(transactionRequest.ClientIdentifier, transactionRequest.TransactionId);

            return _mapper.Map<Transaction, TransactionDto>(transactionToGet);
        }
    }
}