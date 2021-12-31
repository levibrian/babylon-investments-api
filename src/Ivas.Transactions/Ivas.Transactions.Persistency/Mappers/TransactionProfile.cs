using AutoMapper;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Persistency.Entities;

namespace Ivas.Transactions.Persistency.Mappers
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
                .ForMember(destinationMember =>
                        destinationMember.AssetType, 
                    memberOptions => 
                        memberOptions.MapFrom(src => 
                            src.AssetType.ToString()))
                .ReverseMap();
        }
    }
}
