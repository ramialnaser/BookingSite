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
    public class NoSqlAirlinesController : ApiController
    {
        // an instance of the NoSqlHelper class to use its methods
        private NoSqlHelper noSqlHelper = new NoSqlHelper();

        //Get request to get list of airlines
        public HttpResponseMessage Get()
        {
            List<Airline> airlines = noSqlHelper.GetAirlines();

            // if no airlines nothing will return statuscode not found
            if (airlines.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                // if airlines exist in the table, 
                //a status code ok will be returned along with the list of the transactions
                return Request.CreateResponse(HttpStatusCode.OK, airlines);

            }
        }

        //post request to add an airline
        public HttpResponseMessage Post(Airline airline)
        {
            List<Airline> airlines = noSqlHelper.GetAirlines();

            //to check if there's no airline with the same code in the table
            if (airlines.Find(a => a.Code == airline.Code) == null)
            {
                //in case it doesn't exist then the airline will be added and ok status code is returned
                noSqlHelper.AddAirline(airline);
                return Request.CreateResponse(HttpStatusCode.OK, "Created");
            }
            else
            {
                // in case an airline with the same code exists then it won't be added and cannot be created message is returned
                return Request.CreateResponse(HttpStatusCode.OK, "Cannot be created!");
            }

        }
    }
}
