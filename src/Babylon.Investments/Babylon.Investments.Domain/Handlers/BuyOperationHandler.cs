using System.Linq;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;

namespace Babylon.Investments.Domain.Handlers
{
    public class BuyOperationHandler : IOperationHandler
    {
        private readonly ITransactionRepository _transactionRepository;

        public BuyOperationHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<BuyOperation> HandleAsync(TransactionPostRequest request)
        {
            var companyTransactionHistory =
                (await _transactionRepository.GetByTickerAsync(request.TenantId, request.Ticker))
                .ToList();
            
            var domainObject = new BuyOperation(request, companyTransactionHistory);
            
            await _transactionRepository.Insert(domainObject);

            return domainObject;
        }
    }
}