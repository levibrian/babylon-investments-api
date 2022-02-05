using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Babylon.Investments.Api.Constants;
using Babylon.Investments.Api.Controllers.Base;
using Babylon.Investments.Api.Filters;
using Babylon.Investments.Domain.Cryptography;
using Babylon.Investments.Domain.Dtos;
using Babylon.Investments.Domain.Requests;
using Babylon.Investments.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Api.Controllers
{
    [ApiController] 
    [BabylonAuthorize]
    public class TransactionsInBulkController : BabylonController
    {
        private readonly ITransactionsInBulkService _InvestmentsInBulkService;

        private readonly IMapper _mapper;
        
        private readonly ILogger<TransactionsInBulkController> _logger;

        public TransactionsInBulkController(
            ITransactionsInBulkService InvestmentsInBulkService,
            IAesCipher aesCipher,
            IMapper mapper,
            ILogger<TransactionsInBulkController> logger) : base(aesCipher)
        {
            _InvestmentsInBulkService = InvestmentsInBulkService ??
                                         throw new ArgumentNullException(nameof(InvestmentsInBulkService));
            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost(BabylonApiRoutes.InvestmentsInBulkBaseRoute)]
        public async Task<IActionResult> Post([FromBody] IEnumerable<TransactionPostRequest> InvestmentsToCreate)
        {
            _logger.LogInformation("InvestmentsInBulkController - Called HttpPost Create Endpoint");

            var mappedInvestmentsToCreate = _mapper
                .Map<IEnumerable<TransactionPostRequest>, IEnumerable<TransactionPostDto>>(
                    InvestmentsToCreate)
                .ToList();
            
            foreach (var transaction in mappedInvestmentsToCreate)
            {
                transaction.ClientIdentifier = ClientIdentifier;
            }

            var operation = await _InvestmentsInBulkService.CreateAsync(mappedInvestmentsToCreate);

            _logger.LogInformation($"InvestmentsInBulkController - Create Operation Result: {JsonSerializer.Serialize(operation)}");
            
            return Ok(operation);
        }

        [HttpPost]
        [Route(BabylonApiRoutes.InvestmentsInBulkBaseRoute + "/delete")]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<string> transactionIds)
        {
            _logger.LogInformation("InvestmentsInBulkController - Called HttpPost Delete Endpoint");

            var InvestmentsToDelete = 
                transactionIds
                    .Select(transactionId => 
                        new TransactionDeleteDto()
                        {
                            ClientIdentifier = ClientIdentifier, 
                            TransactionId = transactionId
                        })
                    .ToList();

            var operation = await _InvestmentsInBulkService.DeleteAsync(InvestmentsToDelete);
            
            _logger.LogInformation($"InvestmentsInBulkController - Delete Operation Result: {JsonSerializer.Serialize(operation)}");

            return Ok(operation);
        }
    }
}