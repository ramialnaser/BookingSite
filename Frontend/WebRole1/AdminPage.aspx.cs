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
    public partial class AddAirportValues : System.Web.UI.Page
    {
        // to have only one http client to hanlde all the admin requests
        private HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void AddAirportBtn_Click(object sender, EventArgs e)
        {
            // to make sure the fields are not empty
            if (!string.IsNullOrWhiteSpace(AirportCodeTextBox.Text) && !string.IsNullOrWhiteSpace(CityNameTextBox.Text) &&
               !string.IsNullOrWhiteSpace(LatitudeTextBox.Text) && !string.IsNullOrWhiteSpace(LongitudeTextBox.Text))
            {
                //this will create an airport object
                Airport airpot = new Airport()
                {
                    Code = AirportCodeTextBox.Text,
                    City = CityNameTextBox.Text,
                    Latitude = double.Parse(LatitudeTextBox.Text),
                    Longitude = double.Parse(LongitudeTextBox.Text)
                };
                // to check if it is sql or nosql
                string url;
                if (AdminRadioBtn.SelectedItem.Value==("NoSQL"))
                {
                    url = "http://127.0.0.1:70/nosqlairports/";
                }
                else
                {
                    url = "http://127.0.0.1:70/sqlairports/";
                }
                // post request to add an airport
                var result = client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(airpot),
                    Encoding.UTF8,
                    "application/json")
                    ).Result;

                // the response of the request 
                AirportResult.Text = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                AirportResult.Text = "Make sure the fields are not empty!";
            }
        }

        protected void AddAirlineBtn_Click(object sender, EventArgs e)
        {
            // to make sure the fields are not empty
            if (!string.IsNullOrWhiteSpace(AirlineCodeTextBox.Text) && !string.IsNullOrWhiteSpace(AirlineNameTextBox.Text))
            {
                Airline airline = new Airline()
                {
                    Code = AirlineCodeTextBox.Text,
                    Name = AirlineNameTextBox.Text

                };
                // to check if it is sql or nosql
                string url;
                if (AdminRadioBtn.SelectedItem.Value == "NoSQL")
                {
                    url = "http://127.0.0.1:70/nosqlairlines/";
                }
                else
                {
                    url = "http://127.0.0.1:70/sqlairlines/";
                }

                // post request to add an airline
                var result = client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(airline),
                    Encoding.UTF8,
                    "application/json")
                    ).Result;

                // the response of the request 
                AirlineResult.Text = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                AirlineResult.Text = "Make sure the fields are not emtpy";
            }

        }

        protected void AddRouteBtn_Click(object sender, EventArgs e)
        {
            // to make sure the fields are not empty
            if (!string.IsNullOrWhiteSpace(FlightNrTextBox.Text) && !string.IsNullOrWhiteSpace(CarrierTextBox.Text) &&
                !string.IsNullOrWhiteSpace(ArrivalTextBox.Text) && !string.IsNullOrWhiteSpace(DepartureTextBox.Text))
            {
                Route route = new Route()
                {
                    FlightNumber = FlightNrTextBox.Text,
                    Carrier = CarrierTextBox.Text,
                    Departure = DepartureTextBox.Text,
                    Arrival = ArrivalTextBox.Text

                };
                // to check if it is sql or nosql
                string url;
                if (AdminRadioBtn.SelectedItem.Value == "NoSQL")
                {
                    url = "http://127.0.0.1:70/nosqlroutes/";
                }
                else
                {
                    url = "http://127.0.0.1:70/sqlroutes/";
                }
                //post request to add a flight
                var result = client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(route),
                    Encoding.UTF8,
                    "application/json")
                    ).Result;

                // the response of the request 
                RouteResult.Text = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                RouteResult.Text = "Make sure the fields are not empty!";
            }
        }
        protected void AddCustomerButton_Click(object sender, EventArgs e)
        {
            // to make sure the fields are not empty
            if (!string.IsNullOrWhiteSpace(CardNrTextBox.Text) && !string.IsNullOrWhiteSpace(HolderNameTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(ExpDateTextBox.Text) && !string.IsNullOrWhiteSpace(BalanceTextBox.Text))
            {
                Customer customer = new Customer()
                {
                    CardNumber = int.Parse(CardNrTextBox.Text),
                    HolderName = HolderNameTextBox.Text,
                    ExpiryDate = ExpDateTextBox.Text,
                    Balance = int.Parse(BalanceTextBox.Text.Trim())

                };

                // to check if it is sql or nosql
                string url;
                if (AdminRadioBtn.SelectedItem.Value == "NoSQL")
                {
                    url = "http://127.0.0.1:90/nosqlcustomers/";
                }
                else
                {
                    url = "http://127.0.0.1:90/sqlcustomers/";
                }

                //post request to add a customer
                var result = client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(customer),
                    Encoding.UTF8,
                    "application/json")
                    ).Result;

                // the response of the request 
                CustomerResult.Text = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                RouteResult.Text = "Make sure the fields are not empty!";
            }

        }
        protected void GetAirportsBtn_Click(object sender, EventArgs e)
        {
           
            GetAirportsAsync().Wait();
        }
        protected void GetAirlinesBtn_Click(object sender, EventArgs e)
        {
            GetAirlinesAsync().Wait();
        }
        protected void GetRoutesBtn_Click(object sender, EventArgs e)
        {
            GetRoutesAsync().Wait();
        }

        protected void GoToFRSButton_Click(object sender, EventArgs e)
        {
            // to move to the client page
            Response.Redirect("ClientPage.aspx");
        }

        // gets a list of airports
        public async Task GetAirportsAsync()
        {
            List<Airport> airports = new List<Airport>();

            // to check if it is sql or nosql
            string airportsUrl;
            if (AdminRadioBtn.SelectedItem.Value == "NoSQL")
            {
                airportsUrl = "http://127.0.0.1:70/nosqlairports/";
            }
            else
            {
                airportsUrl = "http://127.0.0.1:70/sqlairports/";
            }

            //get request to get airports
            var airportResponse = client.GetAsync(airportsUrl
                ).Result;
            //to check if its ok status code
            if (airportResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                //deserializes airports list
                string airportResults = await airportResponse.Content.ReadAsStringAsync();
                airports = JsonConvert.DeserializeObject<List<Airport>>(airportResults);
            }
            else { airports = null; }

            // binds the list to the table
            airportTable.DataSource = airports;
            airportTable.DataBind();
        }

        //gets a list of airlines
        public async Task GetAirlinesAsync()
        {
            List<Airline> airlines = new List<Airline>();

            // to check if it is sql or nosql
            string airlineUrl;
            if (AdminRadioBtn.SelectedItem.Value == "NoSQL")
            {
                airlineUrl = "http://127.0.0.1:70/nosqlairlines/";
            }
            else
            {
                airlineUrl = "http://127.0.0.1:70/sqlairlines/";
            }
            //Get request to get list of airlines
            var airlineResponse = client.GetAsync(airlineUrl
                ).Result;
            //to check the status code
            if (airlineResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                // if ok then it deserializes the object into a list of airlines
                string airlineResults = await airlineResponse.Content.ReadAsStringAsync();
                airlines = JsonConvert.DeserializeObject<List<Airline>>(airlineResults);
            }
            else { airlines = null; }

            airlineTable.DataSource = airlines;
            airlineTable.DataBind();
        }
        // to get a list of routes
        public async Task GetRoutesAsync()
        {
            List<Route> routes = new List<Route>();

            // to check if it is sql or nosql
            string routeUrl;
            if (AdminRadioBtn.SelectedItem.Value == "NoSQL")
            {
                routeUrl = "http://127.0.0.1:70/nosqlroutes/";
            }
            else
            {
                routeUrl = "http://127.0.0.1:70/sqlroutes/";
            }
            //get request to get routes
            var routeResponse = client.GetAsync(routeUrl
                ).Result;
            // to check the status code
            if (routeResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                // if ok then it deserializes the object into a list of routes
                string routeResult = await routeResponse.Content.ReadAsStringAsync();
                routes = JsonConvert.DeserializeObject<List<Route>>(routeResult);
            }

            else { routes = null; }

            routesTable.DataSource = routes;
            routesTable.DataBind();
        }


    }
}


