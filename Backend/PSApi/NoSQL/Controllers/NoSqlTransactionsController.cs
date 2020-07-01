using PSApi.NoSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PSApi.NoSQL.Controllers
{
    public class NoSqlTransactionsController : ApiController
    {
        // an instance of the NoSqlHelper class to use its methods
        private NoSqlHelper noSqlHelper = new NoSqlHelper();

        // get request to get list of transactions in the database
        public HttpResponseMessage Get()
        {
            List<Transaction> transactions = noSqlHelper.GetTransactions();

            // if no transaction nothing will return statuscode not found
            if (transactions.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if transactions exist in the database, 
                //a status code ok will be returned along with the list of the transactions
                return Request.CreateResponse(HttpStatusCode.OK, transactions);

            }
        }

        // post request To add a transaction
        public HttpResponseMessage Post(Transaction transaction)
        {
            List<Customer> customers = noSqlHelper.GetCustomers();
            // checks if that customer exists in the database
            if (customers.Find(a => a.CardNumber == transaction.CardNumber) == null)
            {
                // if the customer does not exist, then no transaction could be made
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if a customer exists a transaction will be created
                noSqlHelper.AddTransaction(transaction);
                return Request.CreateResponse(HttpStatusCode.OK, "Created!");
            }

        }
        // get request to get a list transactions of a specific card number
        public HttpResponseMessage Get(int id)
        {
            List<Transaction> transactions = noSqlHelper.GetTransactions();
            List<Customer> customers = noSqlHelper.GetCustomers();

            //to check if that customer exists, if the customer exist it will check 
            //if the customer has any transactions related to him in the transaction table 
            //if true a list of transaction for that specific customer is returned and status code ok
            if ((customers.Find(a => a.CardNumber == id) != null) && (transactions.Find(a => a.CardNumber == id) != null))
            {
                List<Transaction> specTransactions = noSqlHelper.GetTransactionsOfSpecCustomer(id);
                return Request.CreateResponse(HttpStatusCode.OK, specTransactions);
            }
            else
            {
                // if not then a not found status code is returned.
                return Request.CreateResponse(HttpStatusCode.NotFound);

            }
        }
    }
}
