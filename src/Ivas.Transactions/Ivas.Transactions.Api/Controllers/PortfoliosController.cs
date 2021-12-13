using System;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ivas.Transactions.Api.Controllers
{
    [Route("/ivas/api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfoliosController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService ?? throw new ArgumentNullException(nameof(portfolioService));
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var userPortfolio = await _portfolioService
                .GetPortfolioByUser(userId);
            
            return Ok(userPortfolio);
        }
    }
}