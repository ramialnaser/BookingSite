
namespace FRSApi.SQL.Models
{
    public class Route
    {
        // a model class to represent the route object

        public Route() { }

        public string FlightNumber { get; set; }

        public string Carrier { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }
    }

}
