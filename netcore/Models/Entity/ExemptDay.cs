using System;


namespace congestion.Entity
{
    public class ExemptDay
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public DateTime ExemptDate { get; set; }

        public City City { get; set; }
    }
}