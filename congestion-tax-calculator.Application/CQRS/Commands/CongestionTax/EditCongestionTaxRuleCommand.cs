using AutoMapper;
using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Domain.Entities.Tax;
using MediatR;

namespace congestion_tax_calculator.Application.CQRS.Commands.CongestionTax
{
    public class EditCongestionTaxRuleCommand : IRequest<EditCongestionTaxRuleCommandResponse>
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public decimal TollFee { get; set; }
    }

    public class EditCongestionTaxRuleCommandResponse
    {
        public int CongestionTaxRuleId { get; set; }
    }
    public class EditCongestionTaxRuleCommandHandler : IRequestHandler<EditCongestionTaxRuleCommand, EditCongestionTaxRuleCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditCongestionTaxRuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EditCongestionTaxRuleCommandResponse> Handle(EditCongestionTaxRuleCommand request, CancellationToken cancellationToken)
        {
            var congestionTaxRule = _mapper.Map<CongestionTaxRule>(request);


            await _unitOfWork.CongestionTaxRuleRepository.Update(congestionTaxRule);
            await _unitOfWork.Save();

            var response = new EditCongestionTaxRuleCommandResponse
            {
                CongestionTaxRuleId = congestionTaxRule.Id
            };

            return response;
        }
    }
}
