namespace Babylon.Investments.Domain.Abstractions.Dtos
{
    public class TransactionDeleteDto
    {
        public string ClientIdentifier { get; set; }
        public string TransactionId { get; set; }
    }
}