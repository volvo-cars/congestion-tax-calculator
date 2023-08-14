using System;
using System.Linq;
using congestion.Contract;
using congestion.Model;

namespace congestion.Service;

public class CongestionTaxCalculator : ICongestionTaxCalculator
{
    private readonly CalendarService _calendar;
    private readonly TollTaxRuleSet _tollTaxRuleSet;

    public CongestionTaxCalculator(CalendarService calendar, TollTaxRuleSet tollTaxRuleSet)
    {
        _calendar = calendar;
        _tollTaxRuleSet = tollTaxRuleSet;
    }

    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        var duration = dates.Last().Subtract(dates.First());
        bool calculateWithSingleRuleTax = duration.Minutes <= 60;
        var totalFee = calculateWithSingleRuleTax
              ? GetSingleRuleTax(vehicle, dates)
              : GetGeneralRuleTax(vehicle, dates);
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        return IsTollFree(date, vehicle)
           ? 0
           : _tollTaxRuleSet.GetFee(date);
    }

    protected bool IsTollFree(DateTime date, Vehicle vehicle)
    {
        return IsTollFreeDate(date) || IsTollFreeVehicle(vehicle);
    }

    protected int GetGeneralRuleTax(Vehicle vehicle, DateTime[] dates)
    {
        var totalFee = 0;
        foreach (DateTime date in dates)
        {
            totalFee += GetTollFee(date, vehicle);
            if (totalFee > 60)
                break;
        }
        return totalFee;
    }

    protected int GetSingleRuleTax(Vehicle vehicle, DateTime[] dates)
    {
        var totalFee = 0;
        foreach (DateTime date in dates)
        {
            int tollFee = GetTollFee(date, vehicle);
            if (totalFee < tollFee)
                totalFee = tollFee;
        }
        return totalFee;
    }

    protected bool IsTollFreeDate(DateTime date)
    {
        return _calendar.IsTollFreeDate(date);
    }

    protected bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;

        return vehicle.IsTaxExempt();
    }
}