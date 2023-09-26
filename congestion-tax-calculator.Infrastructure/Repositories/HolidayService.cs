using congestion_tax_calculator.Application.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace congestion_tax_calculator_net_core.Infrastructure.Repositories
{
    public class HolidayService : IHolidayService
    {
        public List<DateTime> GetPublicHolidays(int year, string countryCode)
        {
            throw new NotImplementedException();
        }

        public bool IsPublicHolidays(DateTime entryDate, string countryCode)
        {
            throw new NotImplementedException();
        }

        public bool IsWeekends(DateTime entryDate, string countryCode)
        {
            throw new NotImplementedException();
        }
    }
}
