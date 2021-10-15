using System;
using System.Collections.Generic;
using Volvo.CongestionTax.Domain.ValueObjects;
using Volvo.Domain.SharedKernel;

namespace Volvo.CongestionTax.Domain.Entities
{
    public class CityCongestionTaxRules : AuditableEntity<int>, IHasDomainEvent
    {
        public string CountryCode { get; set; }
        public string City { get; set; }
        public ICollection<Vehicle> TaxExemptVehicles { get; set; } = new List<Vehicle>();
        public ICollection<DateTime> TollFreeDates { get; set; } = new List<DateTime>();
        public ICollection<TimeZoneAmount> TimeZoneAmounts { get; set; } = new List<TimeZoneAmount>();
        public decimal MaxDailyTollAmount { get; set; }
        
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}