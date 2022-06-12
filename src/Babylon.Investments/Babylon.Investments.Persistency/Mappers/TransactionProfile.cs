using AutoMapper;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Persistency.Entities;

namespace Babylon.Investments.Persistency.Mappers
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
