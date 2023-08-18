using congestion.Model;

namespace congestion_tax_calculator.Test.Unit
{
    public class CalendarBuilder
    {
        private int _yare;

        private List<DayOfWeek> _tollFreeDaysOfWeek = new();

        private List<DateTime> _tollFreeDates = new();

        public CalendarBuilder()
        { }

        public CalendarBuilder ForYare(int yare)
        {
            _yare = yare;
            return this;
        }

        public CalendarBuilder WithWeekends(IList<DayOfWeek> dayOfWeeks)
        {
            _tollFreeDaysOfWeek.AddRange(dayOfWeeks);
            return this;
        }

        public CalendarBuilder WithHolidayDates(IList<DateTime> dateTimes)
        {
            _tollFreeDates.AddRange(dateTimes);
            return this;
        }

        public Calendar Build()
        {
            return new Calendar(_yare, _tollFreeDaysOfWeek, _tollFreeDates);
        }
    }
}