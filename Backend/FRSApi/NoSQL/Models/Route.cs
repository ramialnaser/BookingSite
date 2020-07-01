using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.NoSQL.Models
{
    public class Route : TableEntity
    {    // A model class of route object that implements table entity to be used to be added to the cloud table
         // the partition key is routes
         // rowkey will be added when the route is inserted to the table
        public Route()
        {
            this.PartitionKey = "Routes";
        }

        public string FlightNumber { get; set; }

        public string Carrier { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }
    }
}
