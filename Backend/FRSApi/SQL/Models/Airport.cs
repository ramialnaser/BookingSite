using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.SQL.Models
{
    public class Airport
    {        // a model class to represent the airport object

        public Airport() { }

        public string Code { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
