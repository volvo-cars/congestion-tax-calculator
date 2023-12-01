using congestion.Interface;
using congestion.Models.DTO;
using congestion.Infra;
using congestion.Infra.DB;
using congestion.Infra.TaxRules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace congestion.Services
{
    public class CongestionTaxCalculatorService : ICongestionTaxCalculatorService
    {
        private readonly CongestionTaxContext _context;
        private readonly TaxRuleEngine _taxRuleEngine;

        public CongestionTaxCalculatorService(CongestionTaxContext context, string city)
        {
            _context = context;
            _taxRuleEngine = new TaxRuleEngine();
            LoadRulesFromDatabase(city);
        }

        public void AddRule(ITaxRule rule)
        {
            _taxRuleEngine.AddRule(rule);
        }

        public decimal CalculateCongestionTax(DateTime entryTime, DateTime exitTime, string vehicleType)
        {
            var taxPayer = new TaxPayer { EentryTime = entryTime, TaxedAmount = 0, VehicleType = vehicleType, ExitTime = exitTime };

            return _taxRuleEngine.CalculateTax(taxPayer);
        }

        private void LoadRulesFromDatabase(string cityName)
        {
            var city = _context.Cities.Include(c => c.TaxRules)
                                    .Include(c => c.ExemptVehicles)
                                    .Include(c => c.ExemptDays)
                                    .FirstOrDefault(c => c.CityName == cityName);
            if (city != null)
            {
                foreach (var taxRule in city.TaxRules)
                {
                    _taxRuleEngine.AddRule(new TimeBasedTaxRule(taxRule.StartTime, taxRule.EndTime));
                }

                foreach (var exemptVehicle in city.ExemptVehicles)
                {
                    _taxRuleEngine.AddRule(new ExemptVehicleRule(exemptVehicle.VehicleType));
                }

                foreach (var exemptDay in city.ExemptDays)
                {
                    _taxRuleEngine.AddRule(new ExemptDayRule(exemptDay.ExemptDate));
                }
            }

        }
    }
}
