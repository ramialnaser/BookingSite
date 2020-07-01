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

    public class NoSqlAirportsController : ApiController
    {
        // an instance of the NoSqlHelper class to use its methods
        private NoSqlHelper noSqlHelper = new NoSqlHelper();

        //get request to get list of airports
        public HttpResponseMessage Get()
        {
            List<Airport> airports = noSqlHelper.GetAirports();

            // to check whether there are airports or not in the table
            if (airports.Count == 0)
            {
                //if there none, a not found status code is returned
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if there are,  a list of airports and ok status code are returned
                return Request.CreateResponse(HttpStatusCode.OK, airports);

            }
        }

        //post request to add airport
        public HttpResponseMessage Post(Airport airport)
        {
            List<Airport> airports = noSqlHelper.GetAirports();

            // to check if there's an existing airport with the same code
            if (airports.Find(a => a.Code == airport.Code) == null)
            {
                // in case it does not exist a ok status code and created message are returned
                noSqlHelper.AddAirport(airport);
                return Request.CreateResponse(HttpStatusCode.OK, "Created");
            }
            else
            {
                // in case it does, an ok status code and cannoted be created message are returned
                return Request.CreateResponse(HttpStatusCode.OK, "Cannot be created!");
            }
        }
    }
}
