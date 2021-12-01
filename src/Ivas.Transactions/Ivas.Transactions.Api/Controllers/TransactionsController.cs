using System;
using System.Threading.Tasks;
using AutoMapper;
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

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDto createTransactionDto)
        {
            var operation = await _transactionService.CreateAsync(createTransactionDto);
            
            return Ok(operation);
        }

        [HttpDelete("{userId:long}/{transactionId}")]
        public async Task<IActionResult> Delete(long userId, string transactionId)
        {
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
            var transactions = await _transactionService.GetByUserAsync(userId);

            return Ok(transactions);
        }

        [HttpGet("{userId:long}/{transactionId}")]
        public async Task<IActionResult> Get(long userId, string transactionId)
        {
            var transaction = await _transactionService.GetSingleAsync(new TransactionBaseRequest()
            {
                UserId = userId,
                TransactionId = transactionId
            });

            return Ok(transaction);
        }
    }
}