using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Requests;
using Ivas.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ivas.Transactions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService 
                                  ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDto createTransactionDto)
        {
            var operation = await _transactionService.CreateAsync(createTransactionDto);
            
            return Ok(operation);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] TransactionDeleteDto transactionDeleteDto)
        {
            var operation = await _transactionService.DeleteAsync(transactionDeleteDto);

            return Ok(operation);
        }

        [HttpGet("{userId:long}")]
        public async Task<IActionResult> Get(long userId)
        {
            var transactions = await _transactionService.GetByUserAsync(userId);

            return Ok(transactions);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] TransactionGetSingleRequest transactionRequest)
        {
            var transaction = await _transactionService.GetSingleAsync(transactionRequest);

            return Ok(transaction);
        }
    }
}