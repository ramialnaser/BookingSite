using FRSApi.NoSQL.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;


namespace FRSApi.NoSQL
{// This class will handle communications between the cloudstorage and the Api
    // responsible for table creation and operations such as insert, update and get data.
    public class NoSqlHelper
    {
        private CloudStorageAccount cloudStorageAccount;
        private CloudTableClient cloudTableClient;
        private CloudTable table;
        public NoSqlHelper()
        {
            // establish communication with the cloud storage and creates a table in case it doesn't exit
            cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;          
            cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            table = cloudTableClient.GetTableReference("lab3nosqlfrs");
            table.CreateIfNotExists();
        }

        // adds an instance of the airport object to the table with a partionkey airport and rowkey as the airport's code
        public void AddAirport(Airport airport)
        {
            Airport a = new Airport()
            {
                Code = airport.Code,
                City = airport.City,
                Latitude = airport.Latitude,
                Longitude = airport.Longitude
            };
            a.RowKey = airport.Code;


            TableOperation insert = TableOperation.Insert(a);
            table.Execute(insert);
        }

        // gets a list of airports that exists in the table, under partitionkey airports.
        public List<Airport> GetAirports()
        {
            var entities = table.ExecuteQuery(new TableQuery<Airport>()).ToList();
            List<Airport> airports = new List<Airport>();
            foreach (Airport entity in entities)
            {
                if (entity.PartitionKey == "Airports")
                {
                    airports.Add(entity);
                }
            }
            return airports;
        }
        // adds an instance of the airline object to the table with a partionkey airlines and rowkey as the airline's code
        public void AddAirline(Airline airline)
        {
            Airline a = new Airline()
            {
                Code = airline.Code,
                Name = airline.Name
            };
            a.RowKey = airline.Code;

            TableOperation insert = TableOperation.Insert(a);
            table.Execute(insert);
        }
        // gets a list of airline that exists in the table, under partitionkey airlines.
        public List<Airline> GetAirlines()
        {
            var entities = table.ExecuteQuery(new TableQuery<Airline>()).ToList();
            List<Airline> airlines = new List<Airline>();

            foreach (Airline entity in entities)
            {
                if (entity.PartitionKey == "Airlines")
                {
                    airlines.Add(entity);
                }
            }
            return airlines;
        }

        // adds an instance of the route object to the table with a partionkey routes and rowkey as the route's flight number
        public void AddRoute(Route route)
        {
            Route r = new Route()
            {
                FlightNumber = route.FlightNumber,
                Carrier = route.Carrier,
                Departure = route.Departure,
                Arrival = route.Arrival
            };
            r.RowKey = route.FlightNumber;

            TableOperation insert = TableOperation.Insert(r);
            table.Execute(insert);
        }

        // gets a list of routes that exists in the table, under partitionkey routes.
        public List<Route> GetRoutes()
        {
            var entities = table.ExecuteQuery(new TableQuery<Route>()).ToList();
            List<Route> routes = new List<Route>();

            foreach (Route entity in entities)
            {
                if (entity.PartitionKey == "Routes")
                {
                    routes.Add(entity);
                }
            }
            return routes;
        }

        // gets a list of specific routes that exists in the table.
        //first it loops through all the routes that exist in the table to check if the departure code is the same as the method input
        //second if it exists it loops through airports to find that airport that has the same code as the departure code of the route
        // if it exists then the attributes of route object along with the city attribute from airport object are fetched and added to the routewithspecificroute class
        // then it is added to the list of specific routes.
        public List<RouteWithSpecificAirport> GetRoutesOfSpecificAirport(string code)
        {
            List<Route> routes = GetRoutes();
            List<Airport> airports = GetAirports();
            List<RouteWithSpecificAirport> specRoutes = new List<RouteWithSpecificAirport>();

            foreach (Route route in routes)
            {
                if (route.Departure == code)
                {
                    foreach (Airport airport in airports)
                    {
                        if (airport.Code == code)
                        {
                            specRoutes.Add(new RouteWithSpecificAirport()
                            {
                                FlightNumber = route.FlightNumber,
                                Carrier = route.Carrier,
                                Departure = route.Departure,
                                Arrival = route.Arrival,
                                City = airport.City
                            });
                        }
                    }
                }
            }
            return specRoutes;
        }

        // adds an instance of the flight object to the table with a partionkey flights and rowkey as the flight's passport number
        public void AddFlight(Flight flight)
        {
            Flight f = new Flight()
            {
                PassportNumber = flight.PassportNumber,
                PassengerName = flight.PassengerName,
                PassengerType = flight.PassengerType,
                DepartureDate = flight.DepartureDate,
                Price = flight.Price,
                FlightNumber = flight.FlightNumber
            };
            f.RowKey = flight.PassportNumber;

            TableOperation insert = TableOperation.Insert(f);
            table.Execute(insert);
        }

        // gets a list of airports that exists in the table, under partitionkey airports.
        public List<Flight> GetFlights()
        {
            var entities = table.ExecuteQuery(new TableQuery<Flight>()).ToList();
            List<Flight> flights = new List<Flight>();

            foreach (Flight entity in entities)
            {
                if (entity.PartitionKey == "Flights")
                {
                    flights.Add(entity);
                }
            }
            return flights;
        }

        // to get the calculated revenue of a specific flight with a specific departure date.
        // it loops through flights to check if there's a flight that exist with the input flight number and dep date
        //if it exits then searchs for the route of that specific flight number
        // if the route exist then it searches for both the departure airport and arrival airport
        // then it creates an object of the revenue and the summed price for each iteration of the loop 
        //and return a revenue object with the calculatd price along with the name of the departure and arrival airports
        public Revenue GetRevenues(string flightNumber, string departureDate)
        {

            List<Flight> flights = GetFlights();
            Revenue revenue = new Revenue();

            foreach (Flight flight in flights)
            {
                if ((flight.FlightNumber == flightNumber) && (flight.DepartureDate == departureDate))
                {

                    TableOperation getRoute = TableOperation.Retrieve<Route>("Routes", flight.FlightNumber);
                    var routeResult = table.Execute(getRoute);

                    if (routeResult.Result != null)
                    {
                        Route route = (Route)routeResult.Result;

                        TableOperation getDepAirport = TableOperation.Retrieve<Airport>("Airports", route.Departure);
                        var depAirportResult = table.Execute(getDepAirport);


                        TableOperation getArrAirport = TableOperation.Retrieve<Airport>("Airports", route.Arrival);
                        var arrAirportResult = table.Execute(getArrAirport);

                        if ((depAirportResult.Result != null) && (arrAirportResult.Result != null))
                        {
                            Airport depAirport = (Airport)depAirportResult.Result;

                            Airport arrAirport = (Airport)arrAirportResult.Result;

                            revenue.Arrival = arrAirport.City;
                            revenue.Departure = depAirport.City;
                            revenue.TotalSum += flight.Price;

                        }
                    }
                }
            }
            return revenue;
        }

        // to calculate the price of the trip according to longitude and latitude of from destination and the to destination
        // then checks the type of the passenger and according to that it adds the discounts
        //0.2525 is supposed to be hardcoded baserate for all trips.
        public int PriceCalc(double fromLatitude, double fromLongitude, double toLatitude, double toLongitude, string type)
        {
            double price = 0;
            GeoCoordinate fromCord = new GeoCoordinate(fromLatitude, fromLongitude);
            GeoCoordinate toCord = new GeoCoordinate(toLatitude, toLongitude);

            int distance = (int)fromCord.GetDistanceTo(toCord);

            if (type == "Infant")
            {
                price = 0.2525 * distance * 0.1;
            }
            if (type == "Child")
            {
                price = 0.2525 * distance * 0.67;

            }
            if (type == "Adult")
            {
                price = 0.2525 * distance;

            }
            if (type == "Senior")
            {
                price = 0.2525 * distance * 0.75;

            }
            return (int)price;

        }
    }
}
