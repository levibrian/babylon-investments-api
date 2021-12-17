using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Ivas.Transactions.Api.Constants;
using Ivas.Transactions.Api.Controllers.Base;
using Ivas.Transactions.Api.Filters;
using Ivas.Transactions.Domain.Cryptography;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Requests;
using Ivas.Transactions.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// ReSharper disable All

namespace Ivas.Transactions.Api.Controllers
{
    [Route("/ivas/api/[controller]")]
    [ApiController]
    [IvasAuthorize]
    public class TransactionsController : IvasController
    {
        private readonly ITransactionService _transactionService;

        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(
            ITransactionService transactionService,
            IAesCipher aesCipher,
            ILogger<TransactionsController> logger) : base (aesCipher)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionSubmitDto createTransactionDto)
        {
            _logger.LogInformation($"TransactionsController - Requested Create Transaction with Body: { JsonSerializer.Serialize(createTransactionDto) }");

            createTransactionDto.ClientIdentifier = ClientIdentifier;

            var operation = await _transactionService.CreateAsync(createTransactionDto);
            
            return Ok(operation);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(string userId, string transactionId)
        {
            _logger.LogInformation(
                $"TransactionsController - Requested Delete Transaction with parameters: UserId: {userId} TransactionId: {transactionId} ");
            
            var operation = await _transactionService.DeleteAsync(new TransactionSubmitDto()
            {
                UserId = userId,
                ClientIdentifier = ClientIdentifier,
                TransactionId = transactionId
            });

            return Ok(operation);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            _logger.LogInformation(
                $"TransactionsController - Requested Get Many Transactions with parameters: UserId: { userId } ");
            
            var transactions = await _transactionService.GetByClientAndUserAsync(ClientIdentifier, userId);

            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> Get(Guid transactionId)
        {
            _logger.LogInformation(
                $"TransactionsController - Requested Get Single Transaction with parameters: TransactionId: {transactionId} ");
            
            var transaction = await _transactionService.GetSingleAsync(new TransactionBaseRequest()
            {
                ClientIdentifier = ClientIdentifier,
                TransactionId = transactionId.ToString()
            });

            return Ok(transaction);
        }
    }
}