using Ivas.Transactions.Domain.Abstractions.Requests;

namespace Ivas.Transactions.Domain.Requests
{
    public class TransactionBaseRequest : Request
    {
        public string UserId { get; set; }

        public string ClientIdentifier { get; set; }
        
        public string TransactionId { get; set; }
    }
}