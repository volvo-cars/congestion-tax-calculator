using System;
using System.Collections.Generic;
using MediatR;

namespace Volvo.CongestionTax.Application.Commands
{
    public class CalculateCongestionTaxCommand : IRequest<CalculateCongestionTaxCommandResult>
    {
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string VehicleType { get; set; }
        public IList<DateTime> PassagesTimes { get; set; }
    }

    public class CalculateCongestionTaxCommandResult
    {
        public decimal Amount { get; set; }
    }
}