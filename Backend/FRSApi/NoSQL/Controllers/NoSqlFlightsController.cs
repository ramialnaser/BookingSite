using FRSApi.NoSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FRSApi.NoSQL.Controllers
{
    public class NoSqlFlightsController : ApiController
    {
        // an instance of the NoSqlHelper class to use its methods
        private NoSqlHelper noSqlDbHelper = new NoSqlHelper();

        // get request to get list of flights
        public HttpResponseMessage Get()
        {
            List<Flight> flights = noSqlDbHelper.GetFlights();

            // to check if flights exists or not
            if (flights.Count == 0)
            {
                // if it doesn't exist, not found status code is returned
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                //if found ok status code is returned and list of flights
                return Request.CreateResponse(HttpStatusCode.OK, flights);

            }
        }

        // post request to add flights
        public HttpResponseMessage Post(Flight flight)
        {
            List<Airport> airports = noSqlDbHelper.GetAirports();
            List<Route> routes = noSqlDbHelper.GetRoutes();
            List<Flight> flights = noSqlDbHelper.GetFlights();

            // checks if there are a flight with the same passport number
            //checks if there's a route of the specific flight exists too
            if ((flights.Find(a => a.PassportNumber == flight.PassportNumber) == null) &&
                (routes.Find(a => a.FlightNumber == flight.FlightNumber) != null))
            {
               
                Route route = routes.Find(a => a.FlightNumber == flight.FlightNumber);
                Airport fromAirport = airports.Find(a => a.Code == route.Departure);
                Airport toAirport = airports.Find(a => a.Code == route.Arrival);

                // if the route exists  and the flight does not exist, the price is calculated and the flight is added
                flight.Price = noSqlDbHelper.PriceCalc(fromAirport.Latitude, fromAirport.Longitude,
                    toAirport.Latitude, toAirport.Longitude, flight.PassengerType);

                noSqlDbHelper.AddFlight(flight);
                // a ok code is returned a long with the flight price
                return Request.CreateResponse(HttpStatusCode.OK, (int)flight.Price);
            }
            else
            {
                // in case the condition is not met, then a not found status code is returned
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

        }

        // to get the revenue of specific flight with specific dep date
        public HttpResponseMessage Get(string id, string date)
        {
            List<Flight> flights = noSqlDbHelper.GetFlights();
            Revenue revenue = noSqlDbHelper.GetRevenues(id, date);

            // checks if the flight exists
            if ((revenue == null) && (flights.Find(a => a.FlightNumber == id) == null))
            {
                //if it does not exist a not found status code is returned
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if it exists a ok status code and the revenuse are returned
                return Request.CreateResponse(HttpStatusCode.OK, revenue);

            }
        }
    }
}
