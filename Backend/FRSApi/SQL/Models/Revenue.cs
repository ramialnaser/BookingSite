using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.SQL.Models
{
    public class Revenue
    {
        // a model class to represent the revenue object

        public Revenue() { }
        public int TotalSum { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
    }
}
