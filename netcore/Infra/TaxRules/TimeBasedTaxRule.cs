using congestion.Interface;
using congestion.Models.DTO;
using Nager.Holiday;
using System;
using System.Linq;

namespace congestion.Infra.TaxRules
{
    public class TimeBasedTaxRule : ITaxRule
    {
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }

        public TimeBasedTaxRule(TimeSpan startTime, TimeSpan endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            //Amount = amount;
        }

        public decimal CalculateTax(TaxPayer taxPayer)
        {
            // Check if the day is exempt
            if (IsExemptDay(taxPayer.EentryTime) || IsExemptDay(taxPayer.ExitTime))
            {
                taxPayer.TaxedAmount = 0;
            }

            var entryDateTime = taxPayer.EentryTime.TimeOfDay;
            var exitDateTime = taxPayer.ExitTime.TimeOfDay;

            // Check if the time is within the specified range
            if (IsWithinTaxableHours(entryDateTime) && IsWithinTaxableHours(exitDateTime))
            {
                // Calculate the tax amount
                var taxAmount = taxPayer.TaxedAmount;

                // Check if the calculated tax exceeds the maximum amount per day and vehicle
                if (taxAmount > 60)
                {
                    taxPayer.TaxedAmount = 60;
                }

            }

            return taxPayer.TaxedAmount;

        }

        private bool IsExemptDay(DateTime dateTime)
        {
            // Check if the day is a weekend, public holiday, day before a public holiday, or in July
            return dateTime.DayOfWeek == DayOfWeek.Saturday ||
                   dateTime.DayOfWeek == DayOfWeek.Sunday ||
                   IsPublicHoliday(dateTime) ||
                   IsDayBeforePublicHoliday(dateTime) ||
                   dateTime.Month == 7;
        }

        private bool IsPublicHoliday(DateTime dateTime)
        {
            var holidayClient = new HolidayClient();
            var holidays = holidayClient.GetHolidaysAsync(dateTime.Year, "de").Result;
            if (holidays.Any(o => o.Date == dateTime))
            {
                return true;
            }
            return false;
        }

        private bool IsDayBeforePublicHoliday(DateTime dateTime)
        {
            var holidayClient = new HolidayClient();
            var holidays = holidayClient.GetHolidaysAsync(dateTime.Year, "de").Result;
            if (holidays.Any(o => o.Date == dateTime.AddDays(1)))
            {
                return true;
            }
            return false;
        }

        private bool IsWithinTaxableHours(TimeSpan time)
        {
            // Define the fixed taxable hours
            var taxableStartTime = new TimeSpan(6, 0, 0); // 06:00
            var taxableEndTime = new TimeSpan(18, 29, 59); // 18:29:59

            // Check if the given time is within the taxable hours
            return time >= taxableStartTime && time <= taxableEndTime;
        }
        
    }
}
