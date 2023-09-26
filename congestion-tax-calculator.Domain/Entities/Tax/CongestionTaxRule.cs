using congestion_tax_calculator.Domain.Common;

namespace congestion_tax_calculator.Domain.Entities.Tax
{
    public class CongestionTaxRule : BaseEntity<int>
    {
        public int CityId { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public decimal TollFee { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
