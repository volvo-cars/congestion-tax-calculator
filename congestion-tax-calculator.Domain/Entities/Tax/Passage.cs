using congestion_tax_calculator.Domain.Common;

namespace congestion_tax_calculator.Domain.Entities.Tax
{
    public class Passage : BaseEntity
    {
        public int CityId { get; set; }
        public string PassageNumberPlates { get; set; }
        public DateTime PassageTime { get; set; }
        public bool IsTaxExempt { get; set; }
        public VehicleType VehicleType { get; set; }
        public TollFreeVehicles TollFreeVehicles { get; set; }
    }
}
