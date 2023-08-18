using System;
using congestion.Model;

namespace congestion.Contract;

public interface ICalendarRepository
{
    public Calendar Get(int yare);

    void Add(Calendar calendar);

    void Update(Calendar calendar);
}