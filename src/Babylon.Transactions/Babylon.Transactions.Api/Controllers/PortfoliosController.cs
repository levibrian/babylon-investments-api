using System;
using System.Threading.Tasks;
using Babylon.Transactions.Api.Constants;
using Babylon.Transactions.Api.Controllers.Base;
using Babylon.Transactions.Api.Filters;
using Babylon.Transactions.Domain.Cryptography;
using Babylon.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Babylon.Transactions.Api.Controllers
{
    [Route(BabylonApiRoutes.PortfoliosBaseRoute)]
    [ApiController]
    [BabylonAuthorize]
    public class PortfoliosController : BabylonController
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
        [BabylonAuthorize]
        public async Task<IActionResult> Get(string userId)
        {
            _logger.LogInformation("PortfoliosController - Called HttpGet Get Endpoint");
            
            var userPortfolio = await _portfolioService
                .GetPortfolioByUser(ClientIdentifier, userId);
            
            return Ok(userPortfolio);
        }
    }
}