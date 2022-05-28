using AutoMapper;
using Babylon.Investments.Domain.Abstractions.Dtos;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Abstractions.Responses;
using Babylon.Investments.Domain.Objects;

namespace Babylon.Investments.Domain.Mappers
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
                            src.TransactionType.ToString()));

            CreateMap<Transaction, TransactionDto>()
                .ReverseMap();
        }
    }
}