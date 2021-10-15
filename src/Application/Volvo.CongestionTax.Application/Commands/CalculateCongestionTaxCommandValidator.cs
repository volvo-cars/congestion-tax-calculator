﻿using FluentValidation;

namespace Volvo.CongestionTax.Application.Commands
{
    public class CalculateCongestionTaxCommandValidator : AbstractValidator<CalculateCongestionTaxCommand>
    {
        public CalculateCongestionTaxCommandValidator()
        {
            RuleFor(c => c.VehicleType).NotNull().NotEmpty();
            RuleFor(c => c.PassagesTimes).NotNull().NotEmpty();
        }
    }
}