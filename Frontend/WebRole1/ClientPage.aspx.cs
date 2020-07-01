using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRole1.Models;

namespace WebRole1
{
    public partial class FRS : System.Web.UI.Page
    {
        // to have only one http client to hanlde all the admin requests

        private HttpClient client = new HttpClient();
        private List<Customer> customers;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BookFlight_Click(object sender, EventArgs e)
        {
            // to make sure the fields are not empty
            if (!string.IsNullOrWhiteSpace(PassportTextBox.Text) && !string.IsNullOrWhiteSpace(PassengerNameTextBox.Text) &&
               !string.IsNullOrWhiteSpace(FlightNumTextBox.Text) && !string.IsNullOrWhiteSpace(DepartureDateTextBox.Text))
            {
                Flight flight = new Flight()
                {
                    PassportNumber = PassportTextBox.Text,
                    PassengerName = PassengerNameTextBox.Text,
                    FlightNumber = FlightNumTextBox.Text,
                    DepartureDate = DepartureDateTextBox.Text,
                    PassengerType = PassengerTypeList.SelectedValue.ToString()
                };

                // to check if it is sql or nosql
                string url;
                if (ClientRadionButton.SelectedItem.Value == ("NoSQL"))
                {
                    url = "http://127.0.0.1:70/nosqlflights/";
                }
                else
                {
                    url = "http://127.0.0.1:70/sqlflights/";
                }
                //post request to add a flight
                var result = client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(flight),
                    Encoding.UTF8,
                    "application/json")
                    ).Result;

                // to check the status code
                if (result.StatusCode.Equals(HttpStatusCode.OK))
                {
                    // if it is ok, then the price is displayed on the pricetext box
                    // and a flight is created
                    PriceTextBox.Text = result.Content.ReadAsStringAsync().Result;
                    FlightResults.Text = "Created!";
                }
                else
                {
                    FlightResults.Text = "Cannot be created!!";
                }
            }
            else
            {
                FlightResults.Text = "Make sure the fields are not empty!";
            }
        }

        protected void SpecRoutesButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AirCode.Text))
            {
                GetRoutesForSpecificAirportAsync().Wait();
            }
            else
            {
                SpecRouteResults.Text = "Make sure the fields are not empty!";
            }
        }

        protected void GetCustomersBtn_Click(object sender, EventArgs e)
        {
            // to make sure the fields are not empty
            // and to make sure that the user has booked a flight first before starting to pay for it.
            if (!string.IsNullOrWhiteSpace(CardNumberTextBox.Text) && !string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                GetCustomersAsync().Wait();
                // to get a specific customer of the list of the customers
                if (customers.Find(a => a.CardNumber == int.Parse(CardNumberTextBox.Text)) != null)
                {
                    Customer customer = customers.Find(a => a.CardNumber == int.Parse(CardNumberTextBox.Text));

                    CardHolderTextBox.Text = customer.HolderName;
                    ExpiryDateTextBox.Text = customer.ExpiryDate;
                    BalanceTextBox.Text = customer.Balance.ToString();
                    CardNumberTextBox.ReadOnly = true;
                }
            }
            else
            {
                ConfirmBookingResults.Text = "Make sure the fields are not empty and the customer exists!";
            }
        }

        protected void ConfirmBookingBtn_Click(object sender, EventArgs e)
        {
            GetCustomersAsync().Wait();

            //to make sure that the flight is booked and the user that is going to pay is fetched
            if (CardNumberTextBox.ReadOnly == true)
            {
                Customer customer = customers.Find(a => a.CardNumber == int.Parse(CardNumberTextBox.Text));

                // to update the balance of the user
                customer.Balance = customer.Balance - int.Parse(PriceTextBox.Text);
                BalanceTextBox.Text = customer.Balance.ToString();

                // to check if it is sql or nosql
                string url;
                if (ClientRadionButton.SelectedItem.Value == ("NoSQL"))
                {
                    url = "http://127.0.0.1:90/nosqlcustomers/";
                }
                else
                {
                    url = "http://127.0.0.1:90/sqlcustomers/";
                }
                //put request to update the customer's balance
                var result = client.PutAsync(url, new StringContent(
                    JsonConvert.SerializeObject(customer),
                    Encoding.UTF8,
                    "application/json")
                    ).Result;
                // to check the status code
                if (result.StatusCode.Equals(HttpStatusCode.OK))
                {
                    // if it is ok, then the result will be displayed and a new transaction will be made
                    string updateResult = result.Content.ReadAsStringAsync().Result;

                    MakeTransaction(customer);

                }
                else
                {
                    ConfirmBookingResults.Text = "Cannot update balance";
                }
            }
            else
            {
                ConfirmBookingResults.Text = "Connot be created!!";
            }
        }
        // to get the list of card holders
        protected void FetchCardHoldersButton_Click(object sender, EventArgs e)
        {
            GetCustomersAsync().Wait();


            CustomersTable.DataSource = customers;
            CustomersTable.DataBind();
        }

        public void MakeTransaction(Customer customer)
        {
            Transaction transaction = new Transaction()
            {
                CardNumber = customer.CardNumber,
                TransactionDate = DateTime.Now,
                Amount = int.Parse(PriceTextBox.Text)
            };

            // to check if it is sql or nosql
            string transUrl;
            if (ClientRadionButton.SelectedItem.Value == ("NoSQL"))
            {
                transUrl = "http://127.0.0.1:90/nosqltransactions/";
            }
            else
            {
                transUrl = "http://127.0.0.1:90/sqltransactions/";
            }
            // post request to add a transaction
            var transResult = client.PostAsync(transUrl, new StringContent(
                JsonConvert.SerializeObject(transaction),
                Encoding.UTF8,
                "application/json")
                ).Result;
            //checks the status code
            if (transResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                // if ok, then the response will be displayed for the user
                ConfirmBookingResults.Text = transResult.Content.ReadAsStringAsync().Result;
            }
            else
            {
                ConfirmBookingResults.Text = "Transaction failed!!";
            }
        }
        // to get revenuses
        protected void RevButton_Click(object sender, EventArgs e)
        {
            // to make sure the fields are not empty
            if (!string.IsNullOrWhiteSpace(RevFlgithNr.Text) && !string.IsNullOrWhiteSpace(RevDepDate.Text))
            {
                GetRevenueAsync().Wait();
            }
            else
            {
                RevResults.Text = "Make sure the fields are not empty!";
            }
        }

        // to get specific routes
        protected void GetSpecTransBtn_Click(object sender, EventArgs e)
        {
            // to make sure the field is not empty
            if (!string.IsNullOrWhiteSpace(SpecCardNumber.Text))
            {
                GetSpecTransactionsAsync(int.Parse(SpecCardNumber.Text)).Wait();
            }
            else
            {
                SpecTransResults.Text = "Make sure the fields are not empty!";
            }
        }

        //to get customers
        public async Task GetCustomersAsync()
        {
            // to check if it is sql or nosql
            string customersUrl;
            if (ClientRadionButton.SelectedItem.Value == ("NoSQL"))
            {
                customersUrl = "http://127.0.0.1:90/nosqlcustomers/";
            }
            else
            {
                customersUrl = "http://127.0.0.1:90/sqlcustomers/";
            }
            var customersResponse = client.GetAsync(customersUrl
                ).Result;

            //to check the status code
            if (customersResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                // if ok then it deserializes the object into a list of customers
                string customersResults = await customersResponse.Content.ReadAsStringAsync();
                customers = JsonConvert.DeserializeObject<List<Customer>>(customersResults);
            }
            else { customers = null; }

        }

        //to get specific transactions
        public async Task GetSpecTransactionsAsync(int id)
        {
            List<Transaction> specTransactions = new List<Transaction>();

            // to check if it is sql or nosql
            string transUrl;
            if (ClientRadionButton.SelectedItem.Value == ("NoSQL"))
            {
                 transUrl = "http://127.0.0.1:90/nosqltransactions/" + id + "/";
            }
            else
            {
                 transUrl = "http://127.0.0.1:90/sqltransactions/" + id + "/";
            }

            //a get request to get specific transactions
            var transResponse = client.GetAsync(transUrl
                ).Result;

            // to check the status code
            if (transResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                // if ok then it deserializes the object into a list of specific transactions
                string transResults = await transResponse.Content.ReadAsStringAsync();
                specTransactions = JsonConvert.DeserializeObject<List<Transaction>>(transResults);

                SpecTransTable.DataSource = specTransactions;
                SpecTransTable.DataBind();
            }
            else { customers = null; }

        }

        // to get revenue
        public async Task GetRevenueAsync()
        {
            Revenue revenue = new Revenue();

            // to check if it is sql or nosql
            string revUrl;
            if (ClientRadionButton.SelectedItem.Value == ("NoSQL"))
            {
                 revUrl = "http://127.0.0.1:70/nosqlflights/" + RevFlgithNr.Text + "/" + RevDepDate.Text + "/";
            }
            else
            {
                 revUrl = "http://127.0.0.1:70/sqlflights/" + RevFlgithNr.Text + "/" + RevDepDate.Text + "/";
            }
            //get request to get revenue
            var revResponse = client.GetAsync(revUrl
                ).Result;
            // to check status code
            if (revResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                // if ok then it deserializes the object into a revenue
                string revnResults = await revResponse.Content.ReadAsStringAsync();
                revenue = JsonConvert.DeserializeObject<Revenue>(revnResults);

                // display the revenue details into text boxes
                RevFetchedTextBox.Text = revenue.TotalSum.ToString();
                RevFetchedArrCityTextBox.Text = revenue.Arrival;
                RevFetchedDepCityTextBox.Text = revenue.Departure;
              
            }
            else
            {
                revenue = null;
                RevResults.Text = "Not found!";
            }
        }

        //to get specific route
        public async Task GetRoutesForSpecificAirportAsync()
        {
            List<RouteWithSpecificAirport> routeWithSpecificAirports = new List<RouteWithSpecificAirport>();

            // to check if it is sql or nosql
            string specRoutesUrl;
            if (ClientRadionButton.SelectedItem.Value == ("NoSQL"))
            {
                 specRoutesUrl = "http://127.0.0.1:70/nosqlroutes/" + AirCode.Text + "/";
            }
            else
            {
                specRoutesUrl = "http://127.0.0.1:70/sqlroutes/" + AirCode.Text + "/";
            }
            //get request to get specific routes
            var specRouteResponse = client.GetAsync(specRoutesUrl
                ).Result;

            // checks the status code
            if (specRouteResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                // if ok then it deserializes the object into a list of specific routes
                string specRouteResults = await specRouteResponse.Content.ReadAsStringAsync();
                routeWithSpecificAirports = JsonConvert.DeserializeObject<List<RouteWithSpecificAirport>>(specRouteResults);

                SpecRoutesTable.DataSource = routeWithSpecificAirports;
                SpecRoutesTable.DataBind();
            }
            else
            {
                routeWithSpecificAirports = null;
            }
        }

        // to go to the admin page
        protected void GoToAdminButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");

        }
    }
}