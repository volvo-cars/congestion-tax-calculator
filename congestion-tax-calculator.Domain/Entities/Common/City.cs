using congestion_tax_calculator.Domain.Common;
using congestion_tax_calculator.Domain.Entities.Tax;

namespace congestion_tax_calculator.Domain.Entities.Common
{
    public class City : BaseEntity<int>
    {
        public string Name { get; set; }
        public short CountryId { get; set; }
        public bool HasPublicHolidaysExemp { get; set; }
        public bool HasWeekendsExemp { get; set; }
        public List<CongestionTaxRule>? CongestionTaxRules { get; set; }
        public List<TaxExemptionRule>? TaxExemptionRules { get; set; }
        public List<TollFreeSpecificDate>? TollFreeSpecificDates { get; set; }
        public virtual Country Country { get; set; }

    }
}
