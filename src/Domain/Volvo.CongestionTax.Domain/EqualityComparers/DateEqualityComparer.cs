using System;
using System.Collections.Generic;

namespace Volvo.CongestionTax.Domain.EqualityComparers
{
    public class DateEqualityComparer : IEqualityComparer<DateTime>
    {
        public bool Equals(DateTime x, DateTime y)
        {
            return x.Date.Equals(y.Date);
        }

        public int GetHashCode(DateTime obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Date);
            return hashCode.ToHashCode();
        }
    }
}