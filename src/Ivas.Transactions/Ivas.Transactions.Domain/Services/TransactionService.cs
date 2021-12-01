using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Ivas.Transactions.Domain.Services
{
    public interface ITransactionService :
        ICreatableAsyncService<TransactionCreateDto>, 
        IDeletableAsyncService<TransactionDeleteDto>
    {
        Task<IEnumerable<TransactionDto>> GetByUserAsync(long userId);

        Task<TransactionDto> GetSingleAsync(TransactionBaseRequest transactionRequest);
    }
    
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionValidator _transactionValidator;

        private readonly ITransactionRepository _transactionRepository;

        private readonly IMapper _mapper;

        public TransactionService(
            ITransactionValidator transactionValidator,
            ITransactionRepository transactionRepository,
            IMapper mapper)
        {
            _transactionValidator = transactionValidator 
                                    ?? throw new ArgumentNullException(nameof(transactionValidator));
            
            _transactionRepository = transactionRepository 
                                     ?? throw new ArgumentNullException(nameof(transactionRepository));
            
            _mapper = mapper 
                      ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> CreateAsync(TransactionCreateDto dto)
        {
            var validationResult = _transactionValidator.Validate(dto);

            if (validationResult.IsFailure)
            {
                throw new IvasException(string.Join(", ", validationResult.Errors.Select(x => x.Message)));
            }
            
            var domainObject = new TransactionCreate(dto);

            await _transactionRepository.Insert(domainObject);
            
            return Result.Ok(domainObject.TransactionId);
        }

        public async Task<Result> DeleteAsync(TransactionDeleteDto entity)
        {
            var isEntityValid = _transactionValidator.Validate(entity);
            
            if (isEntityValid.IsFailure)
            {
                throw new IvasException(
                    string.Join(
                        ", ", 
                        isEntityValid.Errors.Select(x => x.Message))
                    );
            }

            var transactionToDelete =
                await _transactionRepository.GetByIdAsync(entity.UserId, entity.TransactionId);

            if (transactionToDelete == null)
            {
                throw new IvasException($"Transaction specified does not exist. Please provide a valid transaction");
            }
            
            await _transactionRepository.Delete(transactionToDelete);

            return Result.Ok(transactionToDelete.TransactionId);
        }

        public async Task<IEnumerable<TransactionDto>> GetByUserAsync(long userId)
        {
            var userTransactions =
                (await _transactionRepository.GetByUserAsync(userId)).OrderByDescending(x => x.Date);

            return _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionDto>>(userTransactions);
        }

        public async Task<TransactionDto> GetSingleAsync(TransactionBaseRequest transactionRequest)
        {
            var transactionToGet =
                await _transactionRepository.GetByIdAsync(transactionRequest.UserId, transactionRequest.TransactionId);

            return _mapper.Map<Transaction, TransactionDto>(transactionToGet);
        }
    }
}