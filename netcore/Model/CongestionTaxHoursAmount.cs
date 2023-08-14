using System;

public class TollTaxAmountRule
{
    public TimeOnly From { get; init; }

    public TimeOnly To { get; init; }

    public int Amount { get; init; }

    public bool IsMach(TimeOnly passingTime)
    {
        return passingTime.IsBetween(From, To);
    }
}