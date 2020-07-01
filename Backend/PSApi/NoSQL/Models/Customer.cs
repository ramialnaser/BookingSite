using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSApi.NoSQL.Models
{
    public class Customer : TableEntity
    {
        // A model class of customer object that implements table entity to be used to be added to the cloud table
        // the partition key is customers
        // rowkey will be added when the customer is inserted to the table
        public Customer()
        {
            this.PartitionKey = "Customers";
        }
        public int CardNumber { get; set; }
        public string HolderName { get; set; }
        public string ExpiryDate { get; set; }
        public int Balance { get; set; }
    }

}
