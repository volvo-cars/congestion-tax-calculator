using congestion.Models.DTO;
using System;

namespace congestion.Interface
{
    public interface ITaxRule
    {
        decimal CalculateTax(TaxPayer taxPayer);
    }
}
