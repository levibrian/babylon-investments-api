using AutoMapper;
using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Enums;
using Babylon.Transactions.Domain.Objects;
using Babylon.Transactions.Persistency.Entities;

namespace Babylon.Transactions.Persistency.Mappers
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionEntity>()
                .ForMember(destinationMember =>
                    destinationMember.TransactionType, 
                    memberOptions => 
                        memberOptions.MapFrom(src => 
                            src.TransactionType.ToString()))
                .ReverseMap();
        }
    }
}
