using System.Transactions;
using AutoMapper;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Domain.Abstractions.Enums;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Persistency.Entities;

namespace Ivas.Transactions.Domain.Mappers
{
    public class TransactionCreateProfile : Profile
    {
        public TransactionCreateProfile()
        {
            CreateMap<TransactionCreateDto, TransactionCreate>()
                .ForMember(dest => dest.DomainErrors,
                    opts => opts
                        .MapFrom(src => src.Errors))
                .ReverseMap();
            
            CreateMap<TransactionCreate, TransactionEntity>()
                .ReverseMap();

            CreateMap<TransactionTypeEnum, TransactionTypeEntity>()
                .ForMember(dest => dest.Id, 
                    opts => opts
                        .MapFrom(src => (long)src))
                .ReverseMap();
        }
    }
}
