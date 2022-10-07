using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Babylon.Investments.Api.Constants;
using Babylon.Investments.Api.Controllers.Base;
using Babylon.Investments.Api.Filters;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Cryptography;
using Babylon.Investments.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// ReSharper disable All

namespace Babylon.Investments.Api.Controllers
{
    [Route(BabylonApiRoutes.TransactionsV1BaseRoute)]
    [ApiController]
    [BabylonAuthorize]
    public class TransactionsController : BabylonController
    {
        private readonly ITransactionService _transactionService;

        private readonly IMapper _mapper;

        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(
            ITransactionService transactionService,
            IAesCipher aesCipher,
            IMapper mapper,
            ILogger<TransactionsController> logger) : base (aesCipher)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionPostRequest createTransactionRequest)
        {
            _logger.LogInformation($"InvestmentsController - Requested Create Transaction with Body: { JsonSerializer.Serialize(createTransactionRequest) }, TenantId: { TenantId }");
            
            createTransactionRequest.TenantId = TenantId;

            var operation = await _transactionService.CreateAsync(createTransactionRequest);
            
            return Ok(operation);
        }
        
        [HttpDelete("{transactionId}")]
        public async Task<IActionResult> Delete(Guid transactionId)
        {
            _logger.LogInformation(
                $"InvestmentsController - Requested Delete Transaction with parameters: TransactionId: { transactionId }, TenantId: { TenantId } ");
            
            var operation = await _transactionService.DeleteAsync(new TransactionDeleteRequest()
            {
                TenantId = TenantId,
                TransactionId = transactionId.ToString()
            });

            return Ok(operation);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            _logger.LogInformation(
                $"InvestmentsController - Requested Get Many Investments with parameters: UserId: { userId } for Client: { TenantId } ");
            
            var userTransactionsFromTenant = await _transactionService.GetByClientAndUserAsync(TenantId, userId);

            return Ok(userTransactionsFromTenant);
        }
    }
}