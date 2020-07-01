using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Transaction
    {
        // a model class that represents transaction object

        public Transaction() { }
        public int CardNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
    }

}