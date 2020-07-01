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
    public class NoSqlCustomersController : ApiController
    {
        // an instance of the NoSqlHelper class to use its methods
        private NoSqlHelper noSqlHelper = new NoSqlHelper();

        // get request to get all customers
        public HttpResponseMessage Get()
        {
            List<Customer> customers = noSqlHelper.GetCustomers();

            // if there are no customers in the table, status code not found will be sent
            if (customers.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            //if there are customers, status code ok will be sent along with list of customers
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, customers);

            }
        }

        // post request that adds a customer
        public HttpResponseMessage Post(Customer customer)
        {
            List<Customer> customers = noSqlHelper.GetCustomers();

            // to check if there are no customers with the same card number
            //in case of none a new customer will be created
            if (customers.Find(a => a.CardNumber == customer.CardNumber) == null)
            {
                noSqlHelper.AddCustomer(customer);
                return Request.CreateResponse(HttpStatusCode.OK, "Created");
            }
            // if there are customers already with the same card number, no customers will be created
            // and it will return cannot be created string.
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Cannot be created!");
            }

        }

        // put request to update the customer's balance
        public HttpResponseMessage Put(Customer customer)
        {
            List<Customer> customers = noSqlHelper.GetCustomers();

            // it will check if that customer exists in the table
            if (customers.Find(a => a.CardNumber == customer.CardNumber) == null)
            {
                // if it doesn't exist, no updates will be performed.
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if it exists, then the customer will be sent to update its balance.
                noSqlHelper.UpdateCustomer(customer);
                return Request.CreateResponse(HttpStatusCode.OK, "Updated");
            }
        }
    }
}
