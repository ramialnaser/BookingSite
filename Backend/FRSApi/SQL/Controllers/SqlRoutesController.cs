using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using FRSApi.SQL.Models;
using System.Net;

namespace FRSApi.SQL.Controllers
{
    public class SqlRoutesController : ApiController
    {
        // an instance of the SqlDbHelper class to use its methods
        private SqlDbHelper sqlDbHelper = new SqlDbHelper();

        // get request to get list of routes
        public async Task<HttpResponseMessage> GetAsync()
        {
            List<Route> routes = await sqlDbHelper.GetRoutes();

            //checks if routes exist
            if (routes.Count == 0)
            {
                // if it does not, a not found status code is returned
                return Request.CreateResponse(HttpStatusCode.NotFound);

            }
            else
            {
                // if it exists, a ok status code and list of routes are returned
                return Request.CreateResponse(HttpStatusCode.OK, routes);

            }
        }
        //post request to add a route
        public async Task<HttpResponseMessage> PostAsync(Route route)
        {
            List<Route> routes = await sqlDbHelper.GetRoutes();
            List<Airport> airports = await sqlDbHelper.GetAirports();
            List<Airline> airlines = await sqlDbHelper.GetAirlines();


            // checks if there's a route with the same flight number exists, and if the carrier exists and also the dep airport and arrival airport

            if ((routes.Find(a => a.FlightNumber == route.FlightNumber) == null) &&

                (routes.Find(a => (a.Carrier == route.Carrier)
                && (a.Arrival == route.Arrival)
                && (a.Departure == route.Departure)) == null) &&

                (airports.Find(a => a.Code == route.Arrival) != null) &&
                (airports.Find(a => a.Code == route.Departure) != null) &&
                (airlines.Find(a => a.Code == route.Carrier) != null))
            {
                //if no route with the same flight number exists, and both carrier and dep and arrival airports exists then, the route is added
                // an ok status code and created message are returned
                await sqlDbHelper.AddRoute(route);
                return Request.CreateResponse(HttpStatusCode.OK, "Created");
            }
            else
            {
                //if condition is not met, ok status code and cannot be created message are returned
                return Request.CreateResponse(HttpStatusCode.OK, "Cannot be created!");
            }

        }

        // get request to get list of routes of a specific dep airport
        public async Task<HttpResponseMessage> GetAsync(string id)
        {
            List<Airport> airports = await sqlDbHelper.GetAirports();
            List<Route> routes = await sqlDbHelper.GetRoutes();

            // checks if the airport and route exists
            if ((airports.Find(a => a.Code == id) != null) && (routes.Find(a => a.Departure == id) != null))
            {
                List<RouteWithSpecificAirport> routeWithSpecificAirports = await sqlDbHelper.GetRoutesForSpecificAirport(id);
                // checks if the list of routes is not empty
                if (routeWithSpecificAirports.Count != 0)
                {
                    //if it is not empty, a ok status code and list of specific routes are returned
                    return Request.CreateResponse(HttpStatusCode.OK, routeWithSpecificAirports);
                }
                else
                {
                    //if it is empty, a not found status code is returned
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            else
            {
                // if the condition is not met, a not found status code is returned
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }



        }

    }

}
