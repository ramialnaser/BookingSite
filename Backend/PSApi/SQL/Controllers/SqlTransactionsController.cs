using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using PSApi.SQL.Models;

namespace PSApi.SQL.Controllers
{
    public class SqlTransactionsController : ApiController
    {
        private SqlDbHelper sqlDbHelper = new SqlDbHelper();

        // gets the list of transactions in the table
        public async Task<HttpResponseMessage> GetAsync()
        {
            List<Transaction> transactions = await sqlDbHelper.GetTransactions();

            // if no transaction nothing will return statuscode not found
            if (transactions.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if transactions exist in the table, 
                //a status code ok will be returned along with the list of the transactions
                return Request.CreateResponse(HttpStatusCode.OK, transactions);

            }
        }

        // To add a transaction
        public async Task<HttpResponseMessage> PostAsync(Transaction transaction)
        {
            List<Customer> customers = await sqlDbHelper.GetCustomers();
            // checks if that customer exists in the table
            if (customers.Find(a => a.CardNumber == transaction.CardNumber) == null)
            {
                // if the customer does not exist, then no transaction could be made
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if a customer exists a transaction will be created
                await sqlDbHelper.AddTransaction(transaction);
                return Request.CreateResponse(HttpStatusCode.OK, "Created!");
            }

        }
        // to get a list of specific transactions of a specific card number
        public async Task<HttpResponseMessage> GetAsync(int id)
        {
            List<Transaction> transactions = await sqlDbHelper.GetTransactions();
            List<Customer> customers = await sqlDbHelper.GetCustomers();

            //to check if that customer exists, if the customer exist it will check 
            //if the customer has any transactions related to him in the transaction table 
            //if true a list of transaction for that specific customer is returned and status code ok
            if ((customers.Find(a=>a.CardNumber==id)!=null)&&(transactions.Find(a=>a.CardNumber==id)!=null))
            {
                List<Transaction> specTransactions = await sqlDbHelper.GetSpecificTransactions(id);
                return Request.CreateResponse(HttpStatusCode.OK,specTransactions);
            }
            else
            {
                // if not then a not found status code is returned.
                return Request.CreateResponse(HttpStatusCode.NotFound);

            }
        }
    }
}