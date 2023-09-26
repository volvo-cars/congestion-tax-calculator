using AutoMapper;
using congestion_tax_calculator.Application.CommonResponse;
using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Tax;
using MediatR;

namespace congestion_tax_calculator.Application.CQRS.Commands.TaxExemption
{
    public class SaveTaxExemptionRuleCommand : IRequest<BaseCommandResponse>
    {
        public int CityId { get; set; }
        public VehicleType VehicleType { get; set; }
        public TollFreeVehicles TollFreeVehicles { get; set; }
        public bool IsExempt { get; set; }
    }
    public class SaveTaxExemptionRuleResponse
    {
        public int CongestionTaxRuleId { get; set; }
    }
    public class SaveTaxExemptionRuleHandler : IRequestHandler<SaveTaxExemptionRuleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveTaxExemptionRuleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(SaveTaxExemptionRuleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

                if (TaxExemptionRuleBasaeInfoExist(request.CityId, request.VehicleType, request.TollFreeVehicles))
                {
                    return new BaseCommandResponse()
                    {
                        IsSuccess = false,
                        Message = "City Id & Start Hour & End Hour Exist"
                    };
                }
                var taxExemptionRule = _mapper.Map<TaxExemptionRule>(request);

                await _unitOfWork.TaxExemptionRuleRepository.Add(taxExemptionRule);
                await _unitOfWork.Save();

                return new BaseCommandResponse()
                {
                    IsSuccess = true,
                    Message = "Create a Congestion-Tax-Rule Successfully"
                };
            }

        private bool TaxExemptionRuleBasaeInfoExist(int cityId, VehicleType vehicleType, TollFreeVehicles tollFreeVehicles)
        {
            return _unitOfWork.TaxExemptionRuleRepository.GetAll().Result
                            .Any(x => x.CityId == cityId && x.VehicleType.Equals(vehicleType) && x.TollFreeVehicles.Equals(tollFreeVehicles));
        }
    }

}
