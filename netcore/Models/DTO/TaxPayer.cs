using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.Models.DTO
{
    public class TaxPayer
    {
        public DateTime EentryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public string VehicleType { get; set; }
        public decimal TaxedAmount { get; set; }
    }
}
