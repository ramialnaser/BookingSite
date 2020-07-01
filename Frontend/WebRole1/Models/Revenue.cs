using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Revenue
    {
        // a model class that represents revenue object

        public Revenue() { }
        public int TotalSum { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
    }
}