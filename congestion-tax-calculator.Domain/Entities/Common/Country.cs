using congestion_tax_calculator.Domain.Common;

namespace congestion_tax_calculator.Domain.Entities.Common
{
    public class Country : BaseEntity<short>
    {
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public string Culture { get; set; }
        public short CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

    }
}
