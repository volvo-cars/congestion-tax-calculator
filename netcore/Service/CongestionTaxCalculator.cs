using System;
using System.Linq;
using congestion.Contract;
using congestion.Model;

namespace congestion.Service;

public class CongestionSingleCHargeTaxCalculator : ICongestionTaxCalculator
{
    private readonly ICalendarRepository _calendarRepository;
    private Model.Calendar _calendar;
    private readonly TollTaxRuleSet _tollTaxSettingsSet;

    public CongestionSingleCHargeTaxCalculator(ICalendarRepository calendarRepository, TollTaxRuleSet tollTaxRuleSet)
    {
        _calendarRepository = calendarRepository;
        _tollTaxSettingsSet = tollTaxRuleSet;
    }

    public int GetTax(Vehicle vehicle, DateTime[] passingTimes)
    {
        var duration = passingTimes.Last().Subtract(passingTimes.First());
        bool calculateWithSingleRuleTax = duration.TotalMinutes <= 60;
        var totalFee = calculateWithSingleRuleTax
              ? GetSingleRuleTax(vehicle, passingTimes)
              : GetGeneralRuleTax(vehicle, passingTimes);

        return totalFee > 60
            ? 60
            : totalFee;
    }

    protected int GetTollFee(DateTime passingTime, Vehicle vehicle)
    {
        return _tollTaxSettingsSet.GetFee(passingTime);
    }

    protected bool IsTollFree(DateTime passingTime, Vehicle vehicle)
    {
        return IsTollFreeDate(passingTime) || IsTollFreeVehicle(vehicle);
    }

    protected int GetGeneralRuleTax(Vehicle vehicle, DateTime[] passingTimes)
    {
        var totalFee = 0;
        if (IsTollFree(passingTimes.First(), vehicle)) return totalFee;

        foreach (DateTime date in passingTimes)
        {
            totalFee += GetTollFee(date, vehicle);
            if (totalFee > 60)
                break;
        }
        return totalFee;
    }

    protected int GetSingleRuleTax(Vehicle vehicle, DateTime[] passingTimes)
    {
        var totalFee = 0;
        if (IsTollFree(passingTimes.First(), vehicle)) return totalFee;

        foreach (DateTime date in passingTimes)
        {
            int tollFee = GetTollFee(date, vehicle);
            if (totalFee < tollFee)
                totalFee = tollFee;
        }
        return totalFee;
    }

    protected bool IsTollFreeDate(DateTime passingDate)
    {
        _calendar = _calendarRepository.Get(passingDate.Year);
        var isDayBeforPublicHoliday = _calendar.IsPublicHolidaye(passingDate.AddDays(1));

        return isDayBeforPublicHoliday || _calendar.IsWeekend(passingDate) || _calendar.IsPublicHolidaye(passingDate) || _calendar.IsDayOfMounth(passingDate, Month.July);
    }

    protected bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;

        return vehicle.IsTaxExempt();
    }
}