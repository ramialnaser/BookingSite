using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.SQL.Models
{
    public class RouteWithSpecificAirport
    {
        // a model class to represent the specific routes object

        public RouteWithSpecificAirport() { }
        public string FlightNumber { get; set; }

        public string Carrier { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }
        public string City { get; set; }
    }
}
