using System;

namespace Volvo.CongestionTax.Domain.Entities
{
    public class PublicHoliday : AuditableEntity<int>
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}