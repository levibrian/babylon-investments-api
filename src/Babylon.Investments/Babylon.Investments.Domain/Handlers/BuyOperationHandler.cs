using System.Linq;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Objects.Base;

namespace Babylon.Investments.Domain.Handlers
{
    public class BuyOperationHandler : IOperationHandler
    {
        private readonly ITransactionRepository _transactionRepository;

        public BuyOperationHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> HandleAsync(TransactionPostRequest request)
        {
            var companyTransactionHistory = (await _transactionRepository
                    .GetAsync(request.TenantId))
                .Where(t => t.Ticker.Equals(request.Ticker.ToUpperInvariant()))
                .ToList();
            
            var domainObject = new TransactionCreate(request, companyTransactionHistory);
            
            await _transactionRepository.Insert(domainObject);

            return domainObject;
        }
    }
}