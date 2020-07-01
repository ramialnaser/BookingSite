using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.NoSQL.Models
{
    public class Revenue
    {
        // a model class for the revenue output to the client.
        public Revenue() { }
        public int TotalSum { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
    }

}
