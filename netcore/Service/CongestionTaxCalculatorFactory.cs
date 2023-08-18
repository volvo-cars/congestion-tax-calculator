using congestion.Contract;
using congestion.Model;
using System;
using System.Linq;

namespace congestion.Service;

public class CongestionTaxCalculatorFactory
{
    private readonly ICalendarRepository _calendarRepository;
    private readonly TollTaxRuleSet _tollTaxRuleSet;

    public CongestionTaxCalculatorFactory(ICalendarRepository calendarRepository, TollTaxRuleSet tollTaxRuleSet)
    {
        _calendarRepository = calendarRepository;
        _tollTaxRuleSet = tollTaxRuleSet;
    }

    public ICongestionTaxCalculator Create(DateTime[] vehiclePassTimes)
    {
        var duration = vehiclePassTimes.Last().Subtract(vehiclePassTimes.First());

        if (duration.TotalMinutes <= 60)
            return new CongestionSingleChargeRuleTaxCalculator(_calendarRepository, _tollTaxRuleSet);
        else
            return new CongestionGeneralChargeRuleTaxCalculator(_calendarRepository, _tollTaxRuleSet);
    }
}