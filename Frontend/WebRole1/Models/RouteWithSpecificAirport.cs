using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class RouteWithSpecificAirport
    {
        // a model class that represents specific route object


        public RouteWithSpecificAirport() { }
        public string FlightNumber { get; set; }

        public string Carrier { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }
        public string City { get; set; }
    }

}