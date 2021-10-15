using System.Collections;
using System.Collections.Generic;

namespace Volvo.CongestionTax.Domain.Entities
{
    public class Vehicle : AuditableEntity<int>
    {
        public string Type { get; set; }
        public ICollection<CityCongestionTaxRules> CityCongestionTaxRulesList { get; set; }
    }
}