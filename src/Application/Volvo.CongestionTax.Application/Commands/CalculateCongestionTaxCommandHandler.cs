using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volvo.CongestionTax.Domain.Services;

namespace Volvo.CongestionTax.Application.Commands
{
    public class
        CalculateCongestionTaxCommandHandler : IRequestHandler<CalculateCongestionTaxCommand,
            CalculateCongestionTaxCommandResult>
    {
        private readonly ICongestionTaxService _congestionTaxService;

        public CalculateCongestionTaxCommandHandler(ICongestionTaxService congestionTaxService)
        {
            _congestionTaxService = congestionTaxService;
        }

        public async Task<CalculateCongestionTaxCommandResult> Handle(CalculateCongestionTaxCommand request,
            CancellationToken cancellationToken)
        {
            var amount = await _congestionTaxService.CalculateAsync(request.CountryCode,
                request.City, request.VehicleType, request.PassagesTimes, cancellationToken);

            return new CalculateCongestionTaxCommandResult
            {
                Amount = amount
            };
        }
    }
}