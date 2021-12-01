using AutoMapper;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;

namespace Ivas.Transactions.Domain.Mappers
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ReverseMap();
        }
    }
}