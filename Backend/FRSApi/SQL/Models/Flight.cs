using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.SQL.Models
{
    public class Flight
    {
        // a model class to represent the flight object

        public Flight() { }

        public string PassportNumber { get; set; }
        public string PassengerName { get; set; }
        public string PassengerType { get; set; }
        public string DepartureDate { get; set; }
        public int Price { get; set; }
        public string FlightNumber { get; set; }

    }

}
