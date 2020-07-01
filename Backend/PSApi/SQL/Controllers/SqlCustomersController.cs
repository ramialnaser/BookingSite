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
    public class SqlCustomersController
     : ApiController
    {
        private SqlDbHelper sqlDbHelper = new SqlDbHelper();

        // to get a list of customers
        public async Task<HttpResponseMessage> GetAsync()
        {
            List<Customer> customers = await sqlDbHelper.GetCustomers();

            // if there are no customers in the database, status code not found will be sent
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

        // to add a customer to the database
        public async Task<HttpResponseMessage> PostAsync(Customer customer)
        {
            List<Customer> customers = await sqlDbHelper.GetCustomers();

            // to check if there are no customers with the same card number
            //in case of none a new customer will be created
            if (customers.Find(a => a.CardNumber == customer.CardNumber) == null)
            {
                await sqlDbHelper.AddCustomer(customer);
                return Request.CreateResponse(HttpStatusCode.OK, "Created");
            }
            // if there are customers already with the same card number, no customers will be created
            // and it will return cannot be created string.
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Cannot be created!");
            }

        }

        // to update the customer balance
        public async Task<HttpResponseMessage> PutAsync(Customer customer)
        {
            List<Customer> customers = await sqlDbHelper.GetCustomers();

            // it will check if that customer exists in the database
            if (customers.Find(a => a.CardNumber == customer.CardNumber) == null)
            {
                // if it doesn't exist, no updates will be performed.
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if it exists, then the customer will be sent to update its balance.
                await sqlDbHelper.UpdateCustomer(customer);
                return Request.CreateResponse(HttpStatusCode.OK, "Updated");
            }
        }
    }
}
