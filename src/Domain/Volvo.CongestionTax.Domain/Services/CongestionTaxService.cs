using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volvo.CongestionTax.Domain.Entities;
using Volvo.CongestionTax.Domain.EqualityComparers;
using Volvo.CongestionTax.Domain.Events;
using Volvo.CongestionTax.Domain.Exceptions;
using Volvo.Domain.SharedKernel;
using Volvo.Infrastructure.SharedKernel.Repositories;

namespace Volvo.CongestionTax.Domain.Services
{
    public class CongestionTaxService : ICongestionTaxService
    {
        private readonly IRepository<CityCongestionTaxRules> _cityCongestionTaxRulesRepository;
        private readonly IDomainEventService _domainEventService;
        private readonly IRepository<PublicHoliday> _publicHolidaysRepository;

        public CongestionTaxService(IDomainEventService domainEventService,
            IRepository<CityCongestionTaxRules> cityCongestionTaxRulesRepository,
            IRepository<PublicHoliday> publicHolidaysRepository)
        {
            _domainEventService = domainEventService;
            _cityCongestionTaxRulesRepository = cityCongestionTaxRulesRepository;
            _publicHolidaysRepository = publicHolidaysRepository;
        }

        public async Task<decimal> CalculateAsync(string countryCode,
            string city,
            string vehicleType,
            IList<DateTime> passageDates,
            CancellationToken cancellationToken = default)
        {
            var cityCongestionTaxRules = await GetCityCongestionTaxRulesByCountryCodeAndCity(countryCode, city, cancellationToken);

            if (cityCongestionTaxRules == null) throw new CongestionTaxRulesNotFoundException(countryCode, city);

            if (CheckIfTaxExemptVehicle(cityCongestionTaxRules, vehicleType))
                return 0;

            var publicHolidaysForCountry = await GetPublicHolidaysByCountryCode(countryCode, cancellationToken);

            var totalAmount = Calculate(cityCongestionTaxRules, publicHolidaysForCountry, passageDates);

            await _domainEventService.Publish(new CongestionTaxCalculatedEvent
            {
                City = city,
                VehicleType = vehicleType,
                PassagesTimes = passageDates,
                Amount = totalAmount
            });

            return totalAmount;
        }

        private decimal Calculate(CityCongestionTaxRules cityCongestionTaxRules,
            ICollection<PublicHoliday> publicHolidays, IList<DateTime> passageDates)
        {
            decimal totalAmount = 0;

            var distinctPassageDates = passageDates.Distinct(new DateEqualityComparer());

            var distinctTollFreeDates = cityCongestionTaxRules
                .TollFreeDates
                .Distinct(new DateEqualityComparer())
                .ToList();

            foreach (var distinctPassageDate in distinctPassageDates)
            {
                if (distinctTollFreeDates.Any(d => d.Date == distinctPassageDate.Date)) continue;

                if (IsTaxFreePassageDate(distinctPassageDate, publicHolidays)) continue;

                totalAmount +=
                    GetTotalAmountForADay(cityCongestionTaxRules,
                        passageDates.Where(d => d.Date == distinctPassageDate.Date).ToList());
            }

            return totalAmount;
        }

        private async Task<CityCongestionTaxRules> GetCityCongestionTaxRulesByCountryCodeAndCity(string countryCode, string city,
            CancellationToken cancellationToken)
        {
            return await _cityCongestionTaxRulesRepository.FindOneAsync(c =>
                c.CountryCode == countryCode
                && c.City == city, cancellationToken);
        }

        private async Task<ICollection<PublicHoliday>> GetPublicHolidaysByCountryCode(string countryCode,
            CancellationToken cancellationToken)
        {
            return await _publicHolidaysRepository.FindAsync(h => h.IsActive
                                                                  && h.CountryCode == countryCode, cancellationToken);
        }

        private static bool CheckIfTaxExemptVehicle(CityCongestionTaxRules cityCongestionTaxRules, string vehicleType)
        {
            return cityCongestionTaxRules.TaxExemptVehicles.Any(v => v.IsActive && v.Type == vehicleType);
        }

        private static bool IsTaxFreePassageDate(DateTime passageDate,
            ICollection<PublicHoliday> publicHolidays)
        {
            return passageDate.DayOfWeek == DayOfWeek.Saturday ||
                   passageDate.DayOfWeek == DayOfWeek.Sunday ||
                   publicHolidays.Any(x => x.Date == passageDate.Date) ||
                   publicHolidays.Any(x => x.Date.AddDays(-1) == passageDate.Date);
        }

        private static decimal GetTotalAmountForADay(CityCongestionTaxRules cityCongestionTaxRules,
            IEnumerable<DateTime> passageDates)
        {
            decimal totalDailyAmount = 0;
            decimal previousTollAmount = 0;
            var previousPassageDate = DateTime.MinValue;

            foreach (var currentPassageDate in passageDates)
            {
                var timeZoneAmount = cityCongestionTaxRules.TimeZoneAmounts
                    .First(t => t.TimeZone.IsInTimeZone(currentPassageDate));

                var minutesSinceLastPaidToll = (currentPassageDate - previousPassageDate).TotalMinutes;

                if (minutesSinceLastPaidToll <= 60)
                {
                    if (previousTollAmount < timeZoneAmount.Amount)
                    {
                        totalDailyAmount += timeZoneAmount.Amount - previousTollAmount;
                        previousTollAmount = timeZoneAmount.Amount;
                    }
                }
                else
                {
                    totalDailyAmount += timeZoneAmount.Amount;
                    previousTollAmount = timeZoneAmount.Amount;
                    previousPassageDate = currentPassageDate;
                }

                if (totalDailyAmount >= cityCongestionTaxRules.MaxDailyTollAmount)
                    return cityCongestionTaxRules.MaxDailyTollAmount;
            }

            return totalDailyAmount;
        }
    }
}