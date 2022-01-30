using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Babylon.Transactions.Api.Constants;
using Babylon.Transactions.Api.Controllers.Base;
using Babylon.Transactions.Api.Filters;
using Babylon.Transactions.Domain.Cryptography;
using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Requests;
using Babylon.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Babylon.Transactions.Api.Controllers
{
    [ApiController] 
    [BabylonAuthorize]
    public class TransactionsInBulkController : BabylonController
    {
        private readonly ITransactionsInBulkService _transactionsInBulkService;

        private readonly IMapper _mapper;
        
        private readonly ILogger<TransactionsInBulkController> _logger;

        public TransactionsInBulkController(
            ITransactionsInBulkService transactionsInBulkService,
            IAesCipher aesCipher,
            IMapper mapper,
            ILogger<TransactionsInBulkController> logger) : base(aesCipher)
        {
            _transactionsInBulkService = transactionsInBulkService ??
                                         throw new ArgumentNullException(nameof(transactionsInBulkService));
            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost(BabylonApiRoutes.TransactionsInBulkBaseRoute)]
        public async Task<IActionResult> Post([FromBody] IEnumerable<TransactionPostRequest> transactionsToCreate)
        {
            _logger.LogInformation("TransactionsInBulkController - Called HttpPost Create Endpoint");

            var mappedTransactionsToCreate = _mapper
                .Map<IEnumerable<TransactionPostRequest>, IEnumerable<TransactionPostDto>>(
                    transactionsToCreate)
                .ToList();
            
            foreach (var transaction in mappedTransactionsToCreate)
            {
                transaction.ClientIdentifier = ClientIdentifier;
            }

            var operation = await _transactionsInBulkService.CreateAsync(mappedTransactionsToCreate);

            _logger.LogInformation($"TransactionsInBulkController - Create Operation Result: {JsonSerializer.Serialize(operation)}");
            
            return Ok(operation);
        }

        [HttpPost]
        [Route(BabylonApiRoutes.TransactionsInBulkBaseRoute + "/delete")]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<string> transactionIds)
        {
            _logger.LogInformation("TransactionsInBulkController - Called HttpPost Delete Endpoint");

            var transactionsToDelete = 
                transactionIds
                    .Select(transactionId => 
                        new TransactionDeleteDto()
                        {
                            ClientIdentifier = ClientIdentifier, 
                            TransactionId = transactionId
                        })
                    .ToList();

            var operation = await _transactionsInBulkService.DeleteAsync(transactionsToDelete);
            
            _logger.LogInformation($"TransactionsInBulkController - Delete Operation Result: {JsonSerializer.Serialize(operation)}");

            return Ok(operation);
        }
    }
}