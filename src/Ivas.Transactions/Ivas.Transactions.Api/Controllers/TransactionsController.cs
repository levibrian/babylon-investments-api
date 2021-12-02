using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Requests;
using Ivas.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// ReSharper disable All

namespace Ivas.Transactions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(
            ITransactionService transactionService, 
            ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDto createTransactionDto)
        {
            _logger.LogInformation($"TransactionsController - Requested Create Transaction with Body: { JsonSerializer.Serialize(createTransactionDto) }");
            
            var operation = await _transactionService.CreateAsync(createTransactionDto);
            
            return Ok(operation);
        }

        [HttpDelete("{userId:long}/{transactionId}")]
        public async Task<IActionResult> Delete(long userId, string transactionId)
        {
            _logger.LogInformation(
                $"TransactionsController - Requested Delete Transaction with parameters: UserId: {userId} TransactionId: {transactionId} ");
            
            var operation = await _transactionService.DeleteAsync(new TransactionDeleteDto()
            {
                UserId = userId,
                TransactionId = transactionId
            });

            return Ok(operation);
        }

        [HttpGet("{userId:long}")]
        public async Task<IActionResult> Get(long userId)
        {
            _logger.LogInformation(
                $"TransactionsController - Requested Get Many Transactions with parameters: UserId: { userId } ");
            
            var transactions = await _transactionService.GetByUserAsync(userId);

            return Ok(transactions);
        }

        [HttpGet("{userId:long}/{transactionId}")]
        public async Task<IActionResult> Get(long userId, string transactionId)
        {
            _logger.LogInformation(
                $"TransactionsController - Requested Get Single Transaction with parameters: UserId: {userId} TransactionId: {transactionId} ");
            
            var transaction = await _transactionService.GetSingleAsync(new TransactionBaseRequest()
            {
                UserId = userId,
                TransactionId = transactionId
            });

            return Ok(transaction);
        }
    }
}