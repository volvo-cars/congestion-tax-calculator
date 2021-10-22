using System;

namespace Volvo.CongestionTax.Domain.Exceptions
{
    public class CongestionTaxRulesNotFoundException : Exception
    {
        public CongestionTaxRulesNotFoundException(string countryCode, string city) : base($"Congestion Tax Rules could not be found for country: {countryCode} and city: {city}")
        {
        }
    }
}