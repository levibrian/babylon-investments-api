using AutoMapper;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Domain.Responses;

namespace Ivas.Transactions.Domain.Mappers
{
    public class TransactionSummaryProfile : Profile
    {
        public TransactionSummaryProfile()
        {
            CreateMap<TransactionSummary, TransactionSummaryGetResponse>();
        }
    }
}