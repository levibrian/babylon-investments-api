using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.Models;
using Babylon.Transactions.Api.Constants;
using Babylon.Transactions.Api.Controllers.Base;
using Babylon.Transactions.Api.Filters;
using Babylon.Transactions.Domain.Cryptography;
using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Requests;
using Babylon.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// ReSharper disable All

namespace Babylon.Transactions.Api.Controllers
{
    [Route(BabylonApiRoutes.TransactionsBaseRoute)]
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
        public async Task<IActionResult> Create([FromBody] TransactionPostRequest createTransactionDto)
        {
            _logger.LogInformation($"TransactionsController - Requested Create Transaction with Body: { JsonSerializer.Serialize(createTransactionDto) }, ClientIdentifier: { ClientIdentifier }");
            
            var transactionDto = _mapper.Map<TransactionPostRequest, TransactionPostDto>(createTransactionDto);

            transactionDto.ClientIdentifier = ClientIdentifier;

            var operation = await _transactionService.CreateAsync(transactionDto);
            
            return Ok(operation);
        }
        
        [HttpDelete("{transactionId}")]
        public async Task<IActionResult> Delete(Guid transactionId)
        {
            _logger.LogInformation(
                $"TransactionsController - Requested Delete Transaction with parameters: TransactionId: { transactionId }, ClientIdentifier { ClientIdentifier } ");
            
            var operation = await _transactionService.DeleteAsync(new TransactionDeleteDto()
            {
                ClientIdentifier = ClientIdentifier,
                TransactionId = transactionId.ToString()
            });

            return Ok(operation);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            _logger.LogInformation(
                $"TransactionsController - Requested Get Many Transactions with parameters: UserId: { userId } for Client: { ClientIdentifier } ");
            
            var transactions = await _transactionService.GetByClientAndUserAsync(ClientIdentifier, userId);

            return Ok(transactions);
        }
    }
}