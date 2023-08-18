using System;

public class TollTaxRule
{
    public TimeOnly From { get; private set; }

    public TollTaxRule(TimeOnly from, TimeOnly to, int amount)
    {
        From = from;
        To = to;
        Amount = amount;
    }

    public TimeOnly To { get; private set; }

    public int Amount { get; private set; }

    public bool IsMach(TimeOnly passingTime)
    {
        return passingTime.IsBetween(From, To);
    }
}