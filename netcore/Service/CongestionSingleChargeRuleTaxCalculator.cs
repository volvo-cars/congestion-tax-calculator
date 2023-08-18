using System;
using System.Linq;
using congestion.Contract;
using congestion.Model;

namespace congestion.Service;

public class CongestionSingleChargeRuleTaxCalculator : CongestionTaxCalculator, ICongestionTaxCalculator
{
    public CongestionSingleChargeRuleTaxCalculator(ICalendarRepository calendarRepository, TollTaxRuleSet tollTaxRuleSet)
       : base(calendarRepository, tollTaxRuleSet) { }

    public int GetTax(Vehicle vehicle, DateTime[] passingTimes)
    {
        return GetSingleRuleTax(vehicle, passingTimes);
    }

    protected int GetSingleRuleTax(Vehicle vehicle, DateTime[] vehiclePassTimes)
    {
        var totalFee = 0;
        if (IsTollFree(vehiclePassTimes.First(), vehicle)) return totalFee;

        foreach (DateTime date in vehiclePassTimes)
        {
            int tollFee = GetTollFee(date, vehicle);
            if (totalFee < tollFee)
                totalFee = tollFee;
        }
        return totalFee;
    }
}