using AutoMapper;
using Ivas.Transactions.Core.Dtos;
using Ivas.Transactions.Core.Enums;
using Ivas.Transactions.Entities.Models;

namespace Ivas.Transactions.Core.Mappers
{
    public class TransactionSubmitProfile : Profile
    {
        public TransactionSubmitProfile()
        {
            CreateMap<Transaction, TransactionSubmitDto>()
                .ReverseMap();

            CreateMap<TransactionTypeEnum, TransactionType>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => (long)src))
                .ReverseMap();
        }
    }
}
