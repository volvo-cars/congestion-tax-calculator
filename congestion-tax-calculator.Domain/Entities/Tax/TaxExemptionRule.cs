using congestion_tax_calculator.Domain.Common;

namespace congestion_tax_calculator.Domain.Entities.Tax
{
    public class TaxExemptionRule : BaseEntity
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public VehicleType VehicleType { get; set; }
        public TollFreeVehicles TollFreeVehicles { get; set; }
        public bool IsExempt { get; set; }

    }
}
