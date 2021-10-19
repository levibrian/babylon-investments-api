using AutoMapper;
using Ivas.Common.Exceptions.Custom;
using Ivas.Common.Extensions;
using Ivas.Core.Services;
using Ivas.Persistency.UnitOfWork.Interfaces;
using Ivas.Transactions.Core.Dtos;
using Ivas.Transactions.Core.Interfaces.Services;
using Ivas.Transactions.Core.Interfaces.Validators;
using Ivas.Transactions.Entities.Models;
using System;
using System.Threading.Tasks;

namespace Ivas.Transactions.Core.Services
{
    public class TransactionsService : ReadOnlyAsyncService<Transaction, TransactionDto>, ITransactionsService
    {
        private readonly ITransactionsCreateValidator _transactionsCreateValidator;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public TransactionsService(ITransactionsCreateValidator transactionsCreateValidator,
                                   IUnitOfWork unitOfWork, 
                                   IMapper mapper) : base(unitOfWork, mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _transactionsCreateValidator = transactionsCreateValidator ?? throw new ArgumentNullException(nameof(transactionsCreateValidator));
        }

        public async Task<long> CreateAsync(TransactionSubmitDto entity)
        {
            var validationResult = await _transactionsCreateValidator.ValidateAsync(entity);

            if (!validationResult.IsValid)
                throw new IvasException(validationResult.Errors.ToErrorString());

            var entityToAdd = _mapper.Map<TransactionSubmitDto, Transaction>(entity);

            var addedEntity = await _unitOfWork.RepositoryAsync<Transaction>().Insert(entityToAdd);
            
            await _unitOfWork.CommitAsync();

            return addedEntity.Id;
        }

        public Task<long> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
