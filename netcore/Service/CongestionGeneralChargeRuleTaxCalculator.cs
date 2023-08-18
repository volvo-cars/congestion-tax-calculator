using System;
using System.Linq;
using congestion.Contract;
using congestion.Model;

namespace congestion.Service;

public class CongestionGeneralChargeRuleTaxCalculator : CongestionTaxCalculator, ICongestionTaxCalculator
{
    //TODO get from TollTaxRuleSet config
    private int _maxTotalFeePerDay = 60;

    public CongestionGeneralChargeRuleTaxCalculator(ICalendarRepository calendarRepository, TollTaxRuleSet tollTaxRuleSet)
    : base(calendarRepository, tollTaxRuleSet) { }

    public int GetTax(Vehicle vehicle, DateTime[] passingTimes)
    {
        //how calc tax for passTimes fro two diffrent day?!!
        return GetGeneralRuleTax(vehicle, passingTimes);
    }

    protected int GetGeneralRuleTax(Vehicle vehicle, DateTime[] vehiclePassTimes)
    {
        var totalFee = 0;
        if (IsTollFree(vehiclePassTimes.First(), vehicle)) return totalFee;

        foreach (DateTime date in vehiclePassTimes)
        {
            totalFee += GetTollFee(date, vehicle);
            if (totalFee > _maxTotalFeePerDay)
                break;
        }
        return totalFee > _maxTotalFeePerDay ? _maxTotalFeePerDay : totalFee;
    }
}