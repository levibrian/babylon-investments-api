using System;
using System.Threading.Tasks;
using Ivas.Transactions.Core.Abstractions.Services;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Ivas.Transactions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionCreateService _transactionCreateService;

        public TransactionsController(ITransactionCreateService transactionCreateService)
        {
            _transactionCreateService = transactionCreateService ??
                                        throw new ArgumentNullException(nameof(transactionCreateService));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDto createTransactionDto)
        {
            var operation = await _transactionCreateService.CreateAsync(createTransactionDto);
            
            return Ok();
        }
    }
}