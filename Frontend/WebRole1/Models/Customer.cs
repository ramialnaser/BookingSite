using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Customer
    {
        // a model class that represents customer object

        public Customer() { }
        public int CardNumber { get; set; }
        public string HolderName { get; set; }
        public string ExpiryDate { get; set; }
        public int Balance { get; set; }
    }
}