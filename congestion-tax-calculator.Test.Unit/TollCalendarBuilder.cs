using congestion.Service;

namespace congestion_tax_calculator.Test.Unit
{
    public class TollCalendarBuilder
    {
        private int _yare;

        private List<DayOfWeek> _tollFreeDaysOfWeek = new();

        private List<DateTime> _tollFreeDates = new();

        public TollCalendarBuilder()
        { }

        public TollCalendarBuilder ForYare(int yare)
        {
            _yare = yare;
            return this;
        }

        public TollCalendarBuilder WithFreeTollDayOfWeek(DayOfWeek dayOfWeek)
        {
            _tollFreeDaysOfWeek.Add(dayOfWeek);
            return this;
        }

        public TollCalendarBuilder WithFreeTollDate(DateTime dateTime)
        {
            _tollFreeDates.Add(dateTime);
            return this;
        }

        public CalendarService Build()
        {
            return new CalendarService(_yare, _tollFreeDaysOfWeek, _tollFreeDates);
        }
    }
}