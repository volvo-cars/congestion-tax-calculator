using AutoMapper;
using congestion_tax_calculator.Application.Exceptions;
using congestion_tax_calculator.Application.Interfaces.Repos;
using MediatR;

namespace congestion_tax_calculator.Application.CQRS.Commands.CongestionTax
{
    public class DeleteCongestionTaxRuleCommand : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteCongestionTaxRuleCommandHandler : IRequestHandler<DeleteCongestionTaxRuleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteCongestionTaxRuleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCongestionTaxRuleCommand request, CancellationToken cancellationToken)
        {
            var congestionTaxRule = await _unitOfWork.CongestionTaxRuleRepository.GetById(request.Id);
            if (congestionTaxRule == null)
                throw new NotFoundException(nameof(congestionTaxRule), request.Id);

            congestionTaxRule.IsRemoved = true;
            congestionTaxRule.RemoveTime = DateTime.UtcNow;
            await _unitOfWork.CongestionTaxRuleRepository.Update(congestionTaxRule);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
