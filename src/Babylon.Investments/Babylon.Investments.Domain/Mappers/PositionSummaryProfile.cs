using AutoMapper;
using Babylon.Investments.Domain.Abstractions.Responses;
using Babylon.Investments.Domain.Objects;

namespace Babylon.Investments.Domain.Mappers
{
    public class PositionSummaryProfile : Profile
    {
        public PositionSummaryProfile()
        {
            CreateMap<PositionSummary, PositionSummaryGetResponse>();
        }
    }
}