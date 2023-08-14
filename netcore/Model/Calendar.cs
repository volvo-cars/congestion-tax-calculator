using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace congestion.Model
{
    public class Calendar
    {
        public Calendar(int yare, List<DayOfWeek> tollFreeDaysOfWeek, List<DateTime> tollFreeDates)
        {
            Yare = yare;
            TollFreeDaysOfWeek = new ReadOnlyCollection<DayOfWeek>(tollFreeDaysOfWeek);
            TollFreeDates = new ReadOnlyCollection<DateTime>(tollFreeDates);
        }

        public bool IsTollFreeDate(DateTime date)
        {
            if (TollFreeDaysOfWeek.Any(x => x == (DayOfWeek)date.DayOfYear))
                return true;

            return TollFreeDates.Any(x => x.Date == date.Date);
        }

        public int Yare { get; private set; }

        public ReadOnlyCollection<DayOfWeek> TollFreeDaysOfWeek { get; private set; }

        public ReadOnlyCollection<DateTime> TollFreeDates { get; private set; }
    }
}