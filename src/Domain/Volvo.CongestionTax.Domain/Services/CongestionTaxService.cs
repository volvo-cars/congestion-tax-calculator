using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volvo.CongestionTax.Domain.Entities;
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
            var cityCongestionTaxRules = await _cityCongestionTaxRulesRepository.FindOneAsync(c =>
                c.CountryCode == countryCode
                && c.City == city, cancellationToken);

            if (cityCongestionTaxRules == null) throw new CongestionTaxRulesNotException(countryCode, city);

            var publicHolidaysForCountry = await _publicHolidaysRepository.FindAsync(h => h.IsActive
                && h.CountryCode == countryCode, cancellationToken);
            
            decimal totalAmount = 0;

            if (cityCongestionTaxRules.TaxExemptVehicles.Any(v => v.IsActive && v.Type == vehicleType) || !passageDates.Any())
                return totalAmount;

            var distinctPassageDates =
                passageDates.GroupBy(d => d.ToString("yyyyMMdd"))
                    .Select(g => g.First())
                    .ToList();

            var tollFreeDates = cityCongestionTaxRules.TollFreeDates.GroupBy(d => d.ToString("yyyyMMdd"))
                .Select(g => g.First())
                .ToList();

            foreach (var distinctPassageDate in distinctPassageDates)
            {
                if (tollFreeDates.Any(d=>d.Date == distinctPassageDate.Date)) continue;

                if (IsTaxFreePassageDate(distinctPassageDate, publicHolidaysForCountry)) continue;

                totalAmount +=
                    GetTotalAmountForADay(cityCongestionTaxRules,
                        passageDates.Where(d => d.Date == distinctPassageDate.Date).ToList());
            }

            await _domainEventService.Publish(new CongestionTaxCalculatedEvent
            {
                City = city,
                VehicleType = vehicleType,
                PassagesTimes = passageDates,
                Amount = totalAmount
            });

            return totalAmount;
        }

        private bool IsTaxFreePassageDate(DateTime passageDate,
            ICollection<PublicHoliday> publicHolidays)
        {
            return passageDate.DayOfWeek == DayOfWeek.Saturday ||
                   passageDate.DayOfWeek == DayOfWeek.Sunday ||
                   publicHolidays.Any(x => x.Date == passageDate.Date) ||
                   publicHolidays.Any(x=>x.Date.AddDays(-1) == passageDate.Date);
        }

        private decimal GetTotalAmountForADay(CityCongestionTaxRules cityCongestionTaxRules,
            IEnumerable<DateTime> passageDates)
        {
            decimal totalDailyAmount = 0;
            decimal previousTollAmount = 0;
            DateTime previousPassageDate = DateTime.MinValue;

            foreach (var currentPassageDate in passageDates)
            {
                var timeZoneAmount = cityCongestionTaxRules.TimeZoneAmounts
                    .First(t => t.TimeZone.IsInTimeZone(currentPassageDate));

                var minutesSinceLastPaidToll = (currentPassageDate - previousPassageDate).TotalMinutes;

                if (minutesSinceLastPaidToll <= 60)
                {
                    if (previousTollAmount < timeZoneAmount.Amount)
                    {
                        totalDailyAmount += (timeZoneAmount.Amount - previousTollAmount);
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