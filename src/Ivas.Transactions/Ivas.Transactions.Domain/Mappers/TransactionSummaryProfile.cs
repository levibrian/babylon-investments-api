using AutoMapper;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;

namespace Ivas.Transactions.Domain.Mappers
{
    public class TransactionSummaryProfile : Profile
    {
        public TransactionSummaryProfile()
        {
            CreateMap<TransactionSummary, TransactionSummaryDto>();
        }
    }
}