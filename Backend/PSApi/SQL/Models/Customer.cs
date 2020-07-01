using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSApi.SQL.Models
{
    public class Customer
    {
        public Customer() { }
        public int CardNumber { get; set; }
        public string HolderName { get; set; }
        public string ExpiryDate { get; set; }
        public int Balance { get; set; }
    }

}
