using Babylon.Transactions.Domain.Abstractions.Requests;

namespace Babylon.Transactions.Domain.Requests
{
    public class TransactionBaseRequest : Request
    {
        public string TransactionId { get; set; }
    }
}