using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Domain.Entities.Tax;
using Microsoft.EntityFrameworkCore;

namespace congestion_tax_calculator.Application.Interfaces.Context
{
    public interface IAppDbContext
    {
        DbSet<City> Cities { get; set; }
        DbSet<CongestionTaxRule> CongestionTaxRules { get; set; }
        DbSet<Passage> Passages { get; set; }
        DbSet<TaxExemptionRule> TaxExemptionRules { get; set; }
        DbSet<TollFreeSpecificDate> TollFreeSpecificDates { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Currency> Currencies { get; set; }
    }
}
