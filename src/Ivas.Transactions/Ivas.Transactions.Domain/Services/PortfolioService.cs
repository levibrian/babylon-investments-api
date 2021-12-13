using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ivas.Transactions.Domain.Contracts.Repositories;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;

namespace Ivas.Transactions.Domain.Services
{
    public interface IPortfolioService
    {
        Task<IEnumerable<TransactionSummaryDto>> GetPortfolioByUser(string userId);
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
        
        public async Task<IEnumerable<TransactionSummaryDto>> GetPortfolioByUser(string userId)
        {
            var userTransactions = (await _transactionRepository
                    .GetByUserAsync(userId))
                .GroupBy(x => x.Ticker)
                .ToDictionary(
                    x => x.Key, 
                    t => t.Select(t => t));

            var userPortfolio = 
                userTransactions
                    .Select(grouping => 
                        new TransactionSummary(grouping.Value))
                    .OrderByDescending(x => 
                        x.TotalInvested)
                    .ToList();

            return _mapper.Map<IEnumerable<TransactionSummary>, IEnumerable<TransactionSummaryDto>>(userPortfolio);
        }
    }
}