using AutoMapper;
using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Objects;
using Babylon.Transactions.Domain.Responses;

namespace Babylon.Transactions.Domain.Mappers
{
    public class PositionSummaryProfile : Profile
    {
        public PositionSummaryProfile()
        {
            CreateMap<PositionSummary, PositionSummaryGetResponse>();
        }
    }
}