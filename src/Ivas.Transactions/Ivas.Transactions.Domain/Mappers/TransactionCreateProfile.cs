using System.Transactions;
using AutoMapper;
using Ivas.Transactions.Domain.Abstractions.Enums;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Persistency.Entities;

namespace Ivas.Transactions.Domain.Mappers
{
    public class TransactionCreateProfile : Profile
    {
        public TransactionCreateProfile()
        {
            CreateMap<TransactionCreate, TransactionEntity>()
                .ReverseMap();

            CreateMap<TransactionTypeEnum, TransactionTypeEntity>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => (long)src))
                .ReverseMap();
        }
    }
}
