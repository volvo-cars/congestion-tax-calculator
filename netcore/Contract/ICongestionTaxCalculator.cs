using System;
using congestion.Model;

namespace congestion.Contract;

public interface ICongestionTaxCalculator
{
    int GetTax(Vehicle vehicle, DateTime[] dates);
}