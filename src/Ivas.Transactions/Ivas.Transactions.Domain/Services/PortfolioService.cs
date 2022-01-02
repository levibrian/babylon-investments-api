using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ivas.Transactions.Domain.Contracts.Repositories;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Domain.Responses;

namespace Ivas.Transactions.Domain.Services
{
    public interface IPortfolioService
    {
        Task<IEnumerable<TransactionSummaryGetResponse>> GetPortfolioByUser(string clientIdentifier, string userId);
    }
    
    public class PortfolioService : IPortfolioService
    {
        private readonly ITransactionRepository _transactionRepository;

        private readonly IMapper _mapper;

        public PortfolioService(
            ITransactionRepository transactionRepository,
            IMapper mapper)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<IEnumerable<TransactionSummaryGetResponse>> GetPortfolioByUser(string clientIdentifier, string userId)
        {
            var userTransactions = (await _transactionRepository
                    .GetByClientAsync(clientIdentifier))
                .Where(x => x.UserId.Equals(userId))
                .GroupBy(x => x.Ticker)
                .ToDictionary(
                    x => x.Key, 
                    t => t.Select(tr => tr));

            var userPortfolio = 
                userTransactions
                    .Select(grouping => 
                        new TransactionSummary(grouping.Value))
                    .OrderByDescending(x => 
                        x.TotalInvested)
                    .ToList();

            return _mapper.Map<IEnumerable<TransactionSummary>, IEnumerable<TransactionSummaryGetResponse>>(userPortfolio);
        }
    }
}