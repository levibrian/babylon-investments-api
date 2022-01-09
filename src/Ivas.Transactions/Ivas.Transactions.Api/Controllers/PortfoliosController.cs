using System;
using System.Threading.Tasks;
using Ivas.Transactions.Api.Constants;
using Ivas.Transactions.Api.Controllers.Base;
using Ivas.Transactions.Api.Filters;
using Ivas.Transactions.Domain.Cryptography;
using Ivas.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ivas.Transactions.Api.Controllers
{
    [Route(IvasApiRoutes.PortfoliosBaseRoute)]
    [ApiController]
    [IvasAuthorize]
    public class PortfoliosController : IvasController
    {
        private readonly IPortfolioService _portfolioService;

        private readonly ILogger<PortfoliosController> _logger;

        public PortfoliosController(
            IPortfolioService portfolioService, 
            IAesCipher aesCipher,
            ILogger<PortfoliosController> logger) : base(aesCipher)
        {
            _portfolioService = portfolioService ?? throw new ArgumentNullException(nameof(portfolioService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpGet]
        [IvasAuthorize]
        public async Task<IActionResult> Get(string userId)
        {
            _logger.LogInformation("PortfoliosController - Called HttpGet Get Endpoint");
            
            var userPortfolio = await _portfolioService
                .GetPortfolioByUser(ClientIdentifier, userId);
            
            return Ok(userPortfolio);
        }
    }
}