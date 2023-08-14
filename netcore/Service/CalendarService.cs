using congestion.calculator;
using congestion.Model;
using System;

namespace congestion.Service;

public class CalendarService
{
    private readonly ApplicationDbContxt _dbContxt;

    public CalendarService(ApplicationDbContxt dbContxt)
    {
        _dbContxt = dbContxt;
    }

    public bool IsTollFreeDate(DateTime date)
    {
        var claendar = _dbContxt.Set<Calendar>().Find(date.Year);

        return claendar.IsTollFreeDate(date.Date);
    }

    public void AddCalendar(Calendar calendar)
    {
        _dbContxt.Set<Calendar>().Add(calendar);
        _dbContxt.SaveChanges();
    }

    public void Update(Calendar calandar)
    {
        var existedcalandar = _dbContxt.Set<Calendar>().Find(calandar.Yare);
        _dbContxt.Update(existedcalandar);
        _dbContxt.SaveChanges();
    }
}