using AutoMapper;
using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Objects;
using Babylon.Transactions.Domain.Requests;
using Babylon.Transactions.Domain.Responses;

namespace Babylon.Transactions.Domain.Mappers
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