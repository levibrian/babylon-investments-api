using System;
using System.Threading.Tasks;
using Babylon.Investments.Api.Constants;
using Babylon.Investments.Api.Controllers.Base;
using Babylon.Investments.Api.Filters;
using Babylon.Investments.Domain.Cryptography;
using Babylon.Investments.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Api.Controllers.v1
{
    [Route(BabylonApiRoutes.PortfoliosV1BaseRoute)]
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
            
            var userPortfolio = await _portfolioService.GetPortfolioByUser(TenantId, userId);
            
            return Ok(userPortfolio);
        }
    }
}