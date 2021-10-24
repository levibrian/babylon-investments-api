using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ivas.Common.Exceptions.Custom;
using Ivas.Transactions.Core.Abstractions.Services;
using Ivas.Transactions.Core.Abstractions.Services.Base;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Persistency.Abstractions.UnitOfWork.Interfaces;
using Ivas.Transactions.Persistency.Entities;

namespace Ivas.Transactions.Core.Services
{
    public class TransactionCreateService : AsyncService<TransactionEntity, TransactionCreateDto>, ITransactionCreateService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        
        public TransactionCreateService(IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<long> CreateAsync(TransactionCreateDto dto)
        {
            var domainObject = new TransactionCreate(dto);

            if (domainObject.DomainErrors.Any())
            {
                throw new IvasException(string.Join(", ", domainObject.DomainErrors));
            }

            var transactionEntity = await _unitOfWork
                .RepositoryAsync<TransactionEntity>()
                .Insert(_mapper.Map<TransactionCreate, TransactionEntity>(domainObject));

            await _unitOfWork.CommitAsync();
            
            return transactionEntity.Id;
        }

        public override Task<long> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public override Task<long> UpdateAsync(TransactionCreateDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}