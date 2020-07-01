using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.NoSQL.Models
{
    public class RouteWithSpecificAirport
    {
        // a model class for the route with specific airport used to send the user list of certian routes
        public RouteWithSpecificAirport() { }
        public string FlightNumber { get; set; }

        public string Carrier { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }
        public string City { get; set; }
    }
}
