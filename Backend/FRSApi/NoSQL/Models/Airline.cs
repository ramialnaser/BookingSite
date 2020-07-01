using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.NoSQL.Models
{
    // A model class of airline object that implements table entity to be used to be added to the cloud table
    // the partition key is airlines
    // rowkey will be added when the airline is inserted to the table
    public class Airline : TableEntity
    {
        public Airline()
        {
            this.PartitionKey = "Airlines";
        }

        public string Code { get; set; }
        public string Name { get; set; }

    }

}
