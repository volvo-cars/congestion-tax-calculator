using congestion.calculator;
using congestion.Contract;
using congestion.Model;
using System;

namespace congestion.Service;

public class CalendarRepository : ICalendarRepository
{
    private readonly ApplicationDbContext _dbContxt;

    public CalendarRepository(ApplicationDbContext dbContxt)
    {
        _dbContxt = dbContxt;
    }

    public Calendar Get(int yare)
    {
        return _dbContxt.Set<Calendar>().Find(yare);
    }

    public void Add(Calendar calendar)
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