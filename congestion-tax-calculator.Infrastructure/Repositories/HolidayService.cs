using congestion_tax_calculator.Application.Interfaces.Repos;
using Nager.Date;

namespace congestion_tax_calculator.Infrastructure.Repositories
{
    public class HolidayService : IHolidayService
    {
        public List<DateTime> GetPublicHolidays(int year, string countryCode)
        {
            //var dateSystem = DateSystemFactory.Create();
            var dateSystem = DateSystem.IsWeekend(DateTime.Now, countryCode);
            var publicHolidays = DateSystem.GetPublicHolidays(year, countryCode).Cast<DateTime>().ToList();
            return publicHolidays;
        }
        public bool IsPublicHolidays(DateTime entryDate, string countryCode)
        {
            return DateSystem.IsPublicHoliday(entryDate, countryCode);
        }
        public bool IsWeekends(DateTime entryDate, string countryCode)
        {
            return DateSystem.IsWeekend(entryDate, countryCode);
        }
    }
}
