using congestion_tax_calculator.Domain.Common;

namespace congestion_tax_calculator.Domain.Entities.Common
{
    public class TollFreeSpecificDate : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
