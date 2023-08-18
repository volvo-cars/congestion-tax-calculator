using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace congestion.Model
{
    public enum Month
    {
        Undefined,

        January,

        February,

        March,

        April,

        May,

        June,

        July,

        August,

        September,

        October,

        November,

        December
    }

    public class Calendar
    {
        public Calendar(int yare, List<DayOfWeek> tollFreeDaysOfWeek, List<DateTime> tollFreeDates)
        {
            Yare = yare;
            Weekends = new List<DayOfWeek>(tollFreeDaysOfWeek);
            Holidays = new List<DateTime>(tollFreeDates);
        }

        private Calendar()
        {
        }

        public List<DateTime> Holidays { get; private set; }

        public List<DayOfWeek> Weekends { get; private set; }

        public int Yare { get; private set; }

        public void SetWeekends(List<DayOfWeek> weekends)
        {
            Weekends = weekends;
        }

        public void SetHolidays(List<DateTime> holydays)
        {
            Holidays = holydays;
        }

        public bool IsDayOfMounth(DateTime date, Month month)
        {
            return (Month)date.Month == month;
        }

        public bool IsPublicHolidaye(DateTime date)
        {
            return Holidays.Any(x => x.Date == date.Date);
        }

        public bool IsWeekend(DateTime date)
        {
            return Weekends.Any(x => x == (DayOfWeek)date.DayOfYear);
        }
    }
}