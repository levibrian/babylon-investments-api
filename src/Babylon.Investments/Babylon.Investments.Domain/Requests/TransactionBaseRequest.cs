using Babylon.Investments.Domain.Abstractions.Requests;

namespace Babylon.Investments.Domain.Requests
{
    public class TransactionBaseRequest : Request
    {
        public string TransactionId { get; set; }
    }
}