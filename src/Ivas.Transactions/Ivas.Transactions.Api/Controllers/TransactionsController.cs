using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.Models;
using Ivas.Transactions.Api.Constants;
using Ivas.Transactions.Api.Controllers.Base;
using Ivas.Transactions.Api.Filters;
using Ivas.Transactions.Domain.Cryptography;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Requests;
using Ivas.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// ReSharper disable All

namespace Ivas.Transactions.Api.Controllers
{
    [Route(IvasApiRoutes.TransactionsBaseRoute)]
    [ApiController]
    [IvasAuthorize]
    public class TransactionsController : IvasController
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
            
            doNotBuild
            
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