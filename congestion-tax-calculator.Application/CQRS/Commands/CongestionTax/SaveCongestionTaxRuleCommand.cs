using AutoMapper;
using congestion_tax_calculator.Application.CommonResponse;
using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Application.Validators;
using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Domain.Entities.Tax;
using MediatR;

namespace congestion_tax_calculator.Application.CQRS.Commands.CongestionTax
{
    public class SaveCongestionTaxRuleCommand : IRequest<BaseCommandResponse>
    {
        public int CityId { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public decimal TollFee { get; set; }
    }
    public class SaveCongestionTaxRuleResponse
    {
        public int CongestionTaxRuleId { get; set; }
    }
    public class SaveCongestionTaxRuleHandler : IRequestHandler<SaveCongestionTaxRuleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveCongestionTaxRuleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(SaveCongestionTaxRuleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new SaveCongestionTaxRuleValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.IsValid == false)
            {
                return new BaseCommandResponse()
                {
                    Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList(),
                    IsSuccess = false,
                    Message = "Create Congestion-Tax-Rule Failed"
                };
            }
            else
            {
                
                if (CongestionTaxRuleBasaeInfoExist(request.CityId, request.StartHour, request.EndHour))
                {
                    return new BaseCommandResponse()
                    {
                        Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList(),
                        IsSuccess = false,
                        Message = "City Id & Start Hour & End Hour Exist"
                    };
                }
                var congestionTaxRule = _mapper.Map<CongestionTaxRule>(request);

                await _unitOfWork.CongestionTaxRuleRepository.Add(congestionTaxRule);
                await _unitOfWork.Save();

                return new BaseCommandResponse()
                {
                    IsSuccess = true,
                    Message = "Create a Congestion-Tax-Rule Successfully"
                };
            }
        }

        private bool CongestionTaxRuleBasaeInfoExist(int cityId, int startHour, int endHour)
        {
            return _unitOfWork.CongestionTaxRuleRepository.GetAll().Result
                .Any(x => x.CityId == cityId && x.StartHour == startHour && x.EndHour == endHour);
        }
    }
}
