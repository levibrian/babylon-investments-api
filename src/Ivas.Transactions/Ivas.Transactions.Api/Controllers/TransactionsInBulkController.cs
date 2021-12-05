using System;
using Ivas.Transactions.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ivas.Transactions.Api.Controllers
{
    [Route("/ivas/api/transactions/bulk")]
    [ApiController]
    public class TransactionsInBulkController : ControllerBase
    {
        private readonly ITransactionsInBulkService _transactionsInBulkService;

        public TransactionsInBulkController(ITransactionsInBulkService transactionsInBulkService)
        {
            _transactionsInBulkService = transactionsInBulkService ??
                                         throw new ArgumentNullException(nameof(transactionsInBulkService));
        }
    }
}