using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.NoSQL.Models
{
    // A model class of airport object that implements table entity to be used to be added to the cloud table
    // the partition key is airports
    // rowkey will be added when the airport is inserted to the table
    public class Airport : TableEntity
    {
        public Airport()
        {
            this.PartitionKey = "Airports";
        }

        public string Code { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
