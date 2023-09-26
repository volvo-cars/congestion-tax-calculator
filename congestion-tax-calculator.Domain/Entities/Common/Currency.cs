using congestion_tax_calculator.Domain.Common;

namespace congestion_tax_calculator.Domain.Entities.Common
{
    public class Currency : BaseEntity<short>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get; set; }
    }

}
