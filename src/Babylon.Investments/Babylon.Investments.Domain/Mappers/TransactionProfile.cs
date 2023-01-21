using AutoMapper;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Abstractions.Responses;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Objects.Base;

namespace Babylon.Investments.Domain.Mappers
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionGetResponse>()
                .ForMember(destinationMember =>
                        destinationMember.TransactionType,
                    memberOptions =>
                        memberOptions.MapFrom(src =>
                            src.TransactionType.ToString()));

            CreateMap<BuyOperation, TransactionPostResponse>()
                .ForMember(destinationMember =>
                        destinationMember.TransactionType,
                    memberOptions =>
                        memberOptions.MapFrom(src =>
                            src.TransactionType.ToString()))
                .ForMember(destinationMember =>
                        destinationMember.TransactedPricePerUnit,
                    memberOptions =>
                        memberOptions.MapFrom(src =>
                            src.PricePerUnit))
                .ForMember(destinationMember =>
                        destinationMember.TransactedUnits,
                    memberOptions =>
                        memberOptions.MapFrom(src =>
                            src.Units))
                .ForMember(destinationMember =>
                        destinationMember.Date,
                    memberOptions =>
                        memberOptions.MapFrom(src =>
                            src.Date.ToString("dd/MM/yyyy")));

            CreateMap<Transaction, TransactionPostRequest>()
                .ReverseMap();
        }
    }
}