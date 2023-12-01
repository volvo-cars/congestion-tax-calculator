using System;
using System.Collections.Generic;
using System.Linq;

namespace congestion.Entity
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public List<TaxRule> TaxRules { get; set; }
        public List<ExemptVehicle> ExemptVehicles { get; set; }
        public List<ExemptDay> ExemptDays { get; set; }
    }
}