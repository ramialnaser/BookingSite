using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.SQL.Models
{
    public class Airline
    {
        // a model class to represent the airline object
        public Airline() { }

        public string Code { get; set; }
        public string Name { get; set; }

    }

}
