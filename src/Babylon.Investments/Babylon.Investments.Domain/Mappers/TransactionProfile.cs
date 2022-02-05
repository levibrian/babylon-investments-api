using AutoMapper;
using Babylon.Investments.Domain.Dtos;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Requests;
using Babylon.Investments.Domain.Responses;

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