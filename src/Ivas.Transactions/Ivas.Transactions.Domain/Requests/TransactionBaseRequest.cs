using Ivas.Transactions.Domain.Abstractions.Requests;

namespace Ivas.Transactions.Domain.Requests
{
    public class TransactionBaseRequest : Request
    {
        public string TransactionId { get; set; }
    }
}