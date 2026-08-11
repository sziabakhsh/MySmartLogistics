namespace MyLogistics.Domain.Common.ValueObjects
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public Location() { } // Required for EF Core serialization

        public Location(double latitude, double longitude, string? city = null, string? country = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            City = city;
            Country = country;
        }
    }
}
