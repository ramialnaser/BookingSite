using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSApi.NoSQL.Models
{
    public class Transaction : TableEntity
    {
        // A model class of transactions object that implements table entity to be used to be added to the cloud table
        // the partition key is transactions
        // rowkey will be added when the transactions is inserted to the table

        public Transaction()
        {
          
            this.PartitionKey = "Transactions";
           
        }
        public int CardNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
    }

}
