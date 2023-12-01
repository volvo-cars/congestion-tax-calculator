using congestion.Interface;
using congestion.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.Infra.TaxRules
{
    public class ExemptVehicleRule : ITaxRule
    {
        private readonly string _exemptVehicleType;

        public ExemptVehicleRule(string exemptVehicleType)
        {
            _exemptVehicleType = exemptVehicleType;
        }

        public decimal CalculateTax(TaxPayer taxPayer)
        {
            if (taxPayer.VehicleType == _exemptVehicleType)
                return 0;
            return taxPayer.TaxedAmount;
        }
    }
}
