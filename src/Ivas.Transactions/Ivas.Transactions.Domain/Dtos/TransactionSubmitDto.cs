namespace Ivas.Transactions.Domain.Dtos
{
    public class TransactionSubmitDto : TransactionDto
    {
        public string ClientIdentifier { get; set; }
    }
}