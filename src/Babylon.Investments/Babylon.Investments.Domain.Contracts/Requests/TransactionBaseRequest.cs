using Babylon.Investments.Domain.Contracts.Requests.Base;

namespace Babylon.Investments.Domain.Contracts.Requests
{
    public class TransactionBaseRequest : Request
    {
        public string TransactionId { get; set; }
    }
}