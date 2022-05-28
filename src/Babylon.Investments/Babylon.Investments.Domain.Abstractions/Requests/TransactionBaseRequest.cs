using Babylon.Investments.Domain.Abstractions.Requests.Base;

namespace Babylon.Investments.Domain.Abstractions.Requests
{
    public class TransactionBaseRequest : Request
    {
        public string TransactionId { get; set; }
    }
}