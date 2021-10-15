using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Volvo.CongestionTax.Domain.Services
{
    public interface ICongestionTaxService
    {
        Task<decimal> CalculateAsync(string countryCode,
            string city,
            string vehicleType,
            IList<DateTime> passageDates,
            CancellationToken cancellationToken = default);
    }
}