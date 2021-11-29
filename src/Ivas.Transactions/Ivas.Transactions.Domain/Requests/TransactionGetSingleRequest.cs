using Ivas.Transactions.Domain.Abstractions.Requests;

namespace Ivas.Transactions.Domain.Requests
{
    public class TransactionGetSingleRequest : Request
    {
        public long UserId { get; set; }
        
        public string TransactionId { get; set; }
    }
}