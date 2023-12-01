using congestion.Interface;
using congestion.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.Infra.TaxRules
{
    public class ExemptDayRule : ITaxRule
    {
        private readonly DateTime _exemptDate;

        public ExemptDayRule(DateTime exemptDate)
        {
            _exemptDate = exemptDate.Date;
        }

        public decimal CalculateTax(TaxPayer taxPayer)
        {
            if (taxPayer.EentryTime.Date == _exemptDate || taxPayer.ExitTime.Date == _exemptDate)
            {
                return 0; // Exempt day
            }

            return taxPayer.TaxedAmount; // Not an exempt day
        }

    }
}
