namespace congestion.Entity
{
    public class ExemptVehicle
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string VehicleType { get; set; }

        public City City { get; set; }
    }
}