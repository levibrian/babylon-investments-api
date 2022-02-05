using AutoMapper;
using Babylon.Investments.Domain.Dtos;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Responses;

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