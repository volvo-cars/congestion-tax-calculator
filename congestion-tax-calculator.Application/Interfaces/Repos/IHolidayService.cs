using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion_tax_calculator.Application.Interfaces.Repos
{
    public interface IHolidayService
    {
        List<DateTime> GetPublicHolidays(int year, string countryCode);
        bool IsPublicHolidays(DateTime entryDate, string countryCode);
        bool IsWeekends(DateTime entryDate, string countryCode);
    }
}
