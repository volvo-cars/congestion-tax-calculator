using System;

namespace congestion.Entity
{
    public class TaxRule
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Amount { get; set; }

        public City City { get; set; }
    }
}