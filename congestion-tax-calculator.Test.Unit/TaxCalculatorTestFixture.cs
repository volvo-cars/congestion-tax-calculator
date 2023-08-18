using congestion.calculator;
using congestion.Model;
using congestion.Service;
using Microsoft.EntityFrameworkCore;

namespace congestion_tax_calculator.Test.Unit
{
    public class TaxCalculatorTestFixture : IDisposable
    {
        public TollTaxRuleSet TollTaxRuleSet { get; private set; }
        public List<DayOfWeek> Weekends { get; private set; } = new();
        public List<TollTaxRule> TollTaxRules { get; private set; } = new();
        public List<DateTime> HolyDays { get; private set; } = new();
        public Calendar Calendar { get; private set; }

        public TaxCalculatorTestFixture()
        {
            FillHolidays();
            FillWeekends();
            FillRules();

            CalendarBuilder tollCalendarBuilder = new();
            tollCalendarBuilder
                .ForYare(2013)
                .WithWeekends(Weekends)
                .WithHolidayDates(HolyDays);
            Calendar = tollCalendarBuilder.Build();

            TollTaxRuleSetBuilder tollTaxRuleSetBuilder = new();
            TollTaxRuleSet = tollTaxRuleSetBuilder
                .WithRules(TollTaxRules)
                .Build();
        }

        private void FillHolidays()
        {
            HolyDays.Add(new DateTime(2013, 1, 1));
            HolyDays.Add(new DateTime(2013, 4, 30));
            HolyDays.Add(new DateTime(2013, 6, 21));
            HolyDays.Add(new DateTime(2013, 11, 20));
            HolyDays.Add(new DateTime(2013, 12, 24));
        }

        private void FillWeekends()
        {
            Weekends.Add(DayOfWeek.Saturday);
            Weekends.Add(DayOfWeek.Sunday);
        }

        private void FillRules()
        {
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(6, 0), new TimeOnly(6, 29, 59), 8));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(6, 30), new TimeOnly(6, 59, 59), 13));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(7, 0), new TimeOnly(7, 59, 59), 18));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(8, 0), new TimeOnly(8, 29, 59), 13));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(8, 30), new TimeOnly(14, 59, 59), 8));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(15, 0), new TimeOnly(15, 29, 59), 13));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(15, 30), new TimeOnly(16, 59, 59), 18));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(17, 0), new TimeOnly(17, 59, 59), 13));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(18, 0), new TimeOnly(18, 29, 59), 8));
            TollTaxRules.Add(new TollTaxRule(new TimeOnly(18, 30), new TimeOnly(5, 59, 59), 0));
        }

        public void Dispose()
        {
        }
    }
}