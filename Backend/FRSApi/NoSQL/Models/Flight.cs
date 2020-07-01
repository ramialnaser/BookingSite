using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRSApi.NoSQL.Models
{
    public class Flight : TableEntity
    {
        // A model class of flight object that implements table entity to be used to be added to the cloud table
        // the partition key is flights
        // rowkey will be added when the flight is inserted to the table
        public Flight()
        {
            this.PartitionKey = "Flights";
        }

        public string PassportNumber { get; set; }
        public string PassengerName { get; set; }
        public string PassengerType { get; set; }
        public string DepartureDate { get; set; }
        public int Price { get; set; }
        public string FlightNumber { get; set; }

    }
}
