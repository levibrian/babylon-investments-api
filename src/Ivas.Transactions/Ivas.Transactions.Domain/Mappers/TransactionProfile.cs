using AutoMapper;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Domain.Requests;
using Ivas.Transactions.Domain.Responses;

namespace Ivas.Transactions.Domain.Mappers
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionPostRequest, TransactionPostDto>()
                .ReverseMap();

            CreateMap<Transaction, TransactionGetResponse>()
                .ForMember(destinationMember =>
                        destinationMember.TransactionType,
                    memberOptions =>
                        memberOptions.MapFrom(src =>
                            src.TransactionType.ToString()))
                .ForMember(destinationMember =>
                        destinationMember.AssetType,
                    memberOptions =>
                        memberOptions.MapFrom(src =>
                            src.AssetType.ToString()));
            
            CreateMap<Transaction, TransactionDto>()
                .ReverseMap();
        }
    }
}