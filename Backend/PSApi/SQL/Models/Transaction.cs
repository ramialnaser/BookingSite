using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSApi.SQL.Models
{
    public class Transaction
    {
        public Transaction() { }
        public int CardNumber{ get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
    }

}
