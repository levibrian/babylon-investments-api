using System.Threading.Tasks;
using AutoMapper;
using Ivas.Transactions.Core.Abstractions.Services;
using Ivas.Transactions.Core.Abstractions.Services.Base;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Persistency.Abstractions.UnitOfWork.Interfaces;
using Ivas.Transactions.Persistency.Entities;

namespace Ivas.Transactions.Core.Services
{
    public class TransactionCreateService : AsyncService<TransactionEntity, TransactionCreateDto>, ITransactionCreateService
    {
        public TransactionCreateService(IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override Task<long> CreateAsync(TransactionCreateDto dto)
        {
            throw new System.NotImplementedException();
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