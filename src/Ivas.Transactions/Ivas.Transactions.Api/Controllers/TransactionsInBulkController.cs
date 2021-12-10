using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ivas.Transactions.Api.Controllers
{
    [Route("/ivas/api/transactions/bulk")]
    [ApiController]
    public class TransactionsInBulkController : ControllerBase
    {
        private readonly ITransactionsInBulkService _transactionsInBulkService;

        private readonly ILogger<TransactionsInBulkController> _logger;

        public TransactionsInBulkController(
            ITransactionsInBulkService transactionsInBulkService,
            ILogger<TransactionsInBulkController> logger)
        {
            _transactionsInBulkService = transactionsInBulkService ??
                                         throw new ArgumentNullException(nameof(transactionsInBulkService));
            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<TransactionCreateDto> transactionsToCreate)
        {
            _logger.LogInformation("TransactionsInBulkController - Called HttpPost Create Endpoint");
            
            var operation = await _transactionsInBulkService.CreateAsync(transactionsToCreate);

            _logger.LogInformation($"TransactionsInBulkController - Create Operation Result: {JsonSerializer.Serialize(operation)}");
            
            return Ok(operation);
        }
    }
}