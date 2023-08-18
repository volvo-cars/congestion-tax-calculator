using System;
using System.Linq;
using congestion.Contract;
using congestion.Model;

namespace congestion.Service;

public abstract class CongestionTaxCalculator
{
    protected readonly ICalendarRepository _calendarRepository;
    protected Model.Calendar _calendar;
    protected readonly TollTaxRuleSet _tollTaxSettingsSet;
    protected readonly string[] _tollFreeVehicleTypes = Enum.GetNames(typeof(TollFreeVehicles));

    public CongestionTaxCalculator(ICalendarRepository calendarRepository, TollTaxRuleSet tollTaxRuleSet)
    {
        _calendarRepository = calendarRepository;
        _tollTaxSettingsSet = tollTaxRuleSet;
    }

    protected virtual int GetTollFee(DateTime passingTime, Vehicle vehicle)
    {
        return _tollTaxSettingsSet.GetFee(passingTime);
    }

    protected virtual bool IsTollFree(DateTime passingTime, Vehicle vehicle)
    {
        return IsTollFreeDate(passingTime) || IsTollFreeVehicle(vehicle);
    }

    protected virtual bool IsTollFreeDate(DateTime vehiclePassTime)
    {
        _calendar = _calendarRepository.Get(vehiclePassTime.Year);
        var isDayBeforPublicHoliday = _calendar.IsPublicHolidaye(vehiclePassTime.AddDays(1));
        return isDayBeforPublicHoliday || _calendar.IsWeekend(vehiclePassTime) || _calendar.IsPublicHolidaye(vehiclePassTime) || _calendar.IsDayOfMounth(vehiclePassTime, Month.July);
    }

    protected virtual bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;

        return _tollFreeVehicleTypes.Contains(vehicle.Type);
    }
}