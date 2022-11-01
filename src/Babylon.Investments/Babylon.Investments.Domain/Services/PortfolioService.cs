using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Babylon.Investments.Domain.Abstractions.Responses;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects;

namespace Babylon.Investments.Domain.Services
{
    public interface IPortfolioService
    {
        Task<IEnumerable<PositionSummaryGetResponse>> GetPortfolioByUser(string tenantId, string userId);
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
        
        public async Task<IEnumerable<PositionSummaryGetResponse>> GetPortfolioByUser(string tenantId, string userId)
        {
            var userInvestments = 
                (await _transactionRepository.GetAsync(tenantId))
                    .Where(x => x.UserId.Equals(userId))
                    .GroupBy(x => x.Ticker)
                    .ToDictionary(
                        x => x.Key, 
                        t => t.Select(tr => tr));

            var userPortfolio = 
                userInvestments
                    .Select(grouping => 
                        new PositionSummary(grouping.Value))
                    .OrderByDescending(x => 
                        x.TotalInvested)
                    .ToList();

            return _mapper.Map<IEnumerable<PositionSummary>, IEnumerable<PositionSummaryGetResponse>>(userPortfolio);
        }
    }
}