using AutoMapper;
using congestion_tax_calculator.Application.CQRS.Commands.CongestionTax;
using congestion_tax_calculator.Application.CQRS.Queries.CongestionTax;
using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Domain.Entities.Tax;

namespace congestion_tax_calculator.Application.AppConfigs
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {


            CreateMap<CongestionTaxRule, SaveCongestionTaxRuleCommand>()
                .ReverseMap();
            CreateMap<CongestionTaxRule, EditCongestionTaxRuleCommand>()
               .ReverseMap();
            CreateMap<GetCongestionTaxQuery, Passage>()
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));


        }
    }
}
