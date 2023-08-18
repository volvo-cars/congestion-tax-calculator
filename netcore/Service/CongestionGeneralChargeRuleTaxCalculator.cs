using System;
using System.Linq;
using congestion.Contract;
using congestion.Model;

namespace congestion.Service;

public class CongestionGeneralChargeRuleTaxCalculator : CongestionTaxCalculator, ICongestionTaxCalculator
{
    public CongestionGeneralChargeRuleTaxCalculator(ICalendarRepository calendarRepository, TollTaxRuleSet tollTaxRuleSet)
    : base(calendarRepository, tollTaxRuleSet) { }

    public int GetTax(Vehicle vehicle, DateTime[] passingTimes)
    {
        return GetGeneralRuleTax(vehicle, passingTimes);
    }

    protected int GetGeneralRuleTax(Vehicle vehicle, DateTime[] vehiclePassTimes)
    {
        var totalFee = 0;
        if (IsTollFree(vehiclePassTimes.First(), vehicle)) return totalFee;

        foreach (DateTime date in vehiclePassTimes)
        {
            totalFee += GetTollFee(date, vehicle);
            if (totalFee > 60)
                break;
        }
        return totalFee > 60 ? 60 : totalFee;
    }
}