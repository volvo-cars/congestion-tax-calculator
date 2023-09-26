using FluentValidation;
using congestion_tax_calculator.Application.CQRS.Commands.CongestionTax;

namespace congestion_tax_calculator.Application.Validators
{
    internal class EditCongestionTaxRuleValidator : AbstractValidator<EditCongestionTaxRuleCommand>
    {
        public EditCongestionTaxRuleValidator()
        {
            RuleFor(st => st.StartHour)
                .NotNull().WithMessage("Start Hour cannot be null")
                .NotEmpty().WithMessage("Start Hour cannot be empty");

            RuleFor(en => en.EndHour)
                .NotNull().WithMessage("End Hour cannot be null")
                .NotEmpty().WithMessage("End Hour cannot be empty");

            RuleFor(ci => ci.TollFee)
                .NotNull().WithMessage("Toll Fee cannot be null")
                .NotEmpty().WithMessage("End Hour cannot be empty");
        }

    }
}
