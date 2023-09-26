using AutoMapper;
using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Domain.Entities.Tax;
using MediatR;

namespace congestion_tax_calculator.Application.CQRS.Queries.CongestionTax
{
    public class GetCongestionTaxQuery : IRequest<decimal>
    {
        public int CityId { get; set; }
        public List<DateTime>? PassageTimes { get; set; }
        public decimal MaxAmount { get; set; }
    }

    public class GetCongestionTaxResult
    {
        public decimal TaxAmount { get; set; }
    }

    public class CalculateCongestionTaxQueryHandler : IRequestHandler<GetCongestionTaxQuery, decimal>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHolidayService _holidayService;

        public CalculateCongestionTaxQueryHandler(IUnitOfWork unitOfWork , IMapper mapper, IHolidayService holidayService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _holidayService = holidayService;
        }

        public Task<decimal> Handle(GetCongestionTaxQuery request, CancellationToken cancellationToken)
        {
            var domainModel = _mapper.Map<Passage>(request);
            var city = _unitOfWork.CityRepository.GetById(request.CityId).Result;

            if (city == null)
            {
                throw new Exception("City not found");
            }

            decimal taxAmount = CalculateCongestionTax(city, request.PassageTimes,request.MaxAmount);

            return Task.FromResult(taxAmount);
        }

        private decimal CalculateTaxAmountForPassage(DateTime passageTime, City city)
        {
            // Initialize the tax amount to zero.
            decimal taxAmount = 0;

            // Get the current passage time.
            //DateTime passageTime = passage.PassageTime;

            // Check if the passage time is within the city's congestion tax rules.
            var applicableRule = city.CongestionTaxRules.FirstOrDefault(rule =>
                rule.StartHour <= passageTime.Hour &&
                rule.EndHour >= passageTime.Hour);

            if (applicableRule != null)
            {
                // Apply the toll fee from the applicable congestion tax rule.
                taxAmount = applicableRule.TollFee;
            }
            else
            {
                // Handle cases where there is no applicable rule (e.g., outside congestion tax hours).
                // You can implement custom logic here, such as applying a default tax amount.
                taxAmount = CalculateDefaultTaxAmount();
            }

            //Check for tax exemptions
            //////////if (IsTaxExempt(passage, city))
            //////////    {
            //////////        // Vehicle is exempt from tax
            //////////        return 0;
            //////////    }

            // Apply the tax amount unless the vehicle qualifies for an exemption.
            return taxAmount;
        }

        public decimal CalculateCongestionTax(
            City city,
            List<DateTime> passageTimes,
            decimal maxAmount)
        {
            passageTimes.Sort();

            decimal totalTaxAmount = 0;
            DateTime windowStart = passageTimes[0];
            DateTime windowEnd = windowStart.AddMinutes(60);

            decimal highestTaxAmount = 0;

            foreach (var passageTime in passageTimes)
            {
                // Check if the passage time is within the 60-minute window.
                if (passageTime <= windowEnd)
                {
                    // Check if the passage time is on a public holiday or the day before a public holiday or contains specific dates.
                    var year = passageTime.Year;
                    var country = _unitOfWork.CountryRepository.GetById(city.Id).Result;
                    var publicHolidays = _holidayService.GetPublicHolidays(year, country.IsoCode);
                    var specificDate = _unitOfWork.CityRepository.GetCityTollFreeSpecificDateByIdAsync(city.Id).Result;

                    if (publicHolidays.Contains(passageTime) || publicHolidays.Contains(passageTime.AddDays(-1)) || specificDate.Contains(passageTime))
                    {
                        continue; // Vehicle is exempt on public holidays.
                    }

                    // Calculate the tax amount for this passage.
                    decimal taxAmount = CalculateTaxAmountForPassage(passageTime,city);

                    // Update the highest tax amount if needed.
                    highestTaxAmount = Math.Max(highestTaxAmount, taxAmount);
                }
                else
                {
                    // Move the window to the next 60-minute interval.
                    windowStart = passageTime;
                    windowEnd = windowStart.AddMinutes(60);

                    // Add the highest tax amount for the previous window to the total.
                    totalTaxAmount += highestTaxAmount;

                    // Reset the highest tax amount for the new window.
                    highestTaxAmount = 0;
                }
            }

            // Add the highest tax amount for the last window to the total.
            totalTaxAmount += highestTaxAmount;

            // Apply the maximum daily and per-vehicle tax limit.
            totalTaxAmount = Math.Min(totalTaxAmount, maxAmount);

            return totalTaxAmount;
        }


        private decimal CalculateDefaultTaxAmount()
        {
            throw new NotImplementedException();
        }

        private bool IsTaxExempt(Passage passage, City city)
        {
            throw new NotImplementedException();
        }
    }
}
