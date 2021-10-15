using System;

namespace Volvo.CongestionTax.Domain.Exceptions
{
    public class CongestionTaxRulesNotException : Exception
    {
        public CongestionTaxRulesNotException(string countryCode, string city) : base($"Congestion Tax Rules could not be found for country: {countryCode} and city: {city}")
        {
        }
    }
}