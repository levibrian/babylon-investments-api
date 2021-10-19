using Ivas.Common.ActionFilters;
using Ivas.Transactions.Core.Dtos;
using Ivas.Transactions.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Ivas.Transactions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService ?? throw new ArgumentNullException(nameof(transactionsService));
        }

        // GET: api/<TransactionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionSubmitDto transactionDto)
        {
            var opResult = await _transactionsService.CreateAsync(transactionDto);

            return Ok(new OkObjectResult(opResult));
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
