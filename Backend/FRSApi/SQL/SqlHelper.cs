using FRSApi.SQL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Device.Location;
using System.Threading.Tasks;

namespace FRSApi.SQL
{

    public class SqlDbHelper
    {
        // this class will handle the communications between the database and the controllers using the model classes as objects.

        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=lab3frsdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // to get a list of airports from the database
        public async Task<List<Airport>> GetAirports()
        {
            List<Airport> airports = new List<Airport>();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Airports;";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            airports.Add(new Airport()
                            {
                                Code = reader.GetString(0),
                                City = reader.GetString(1),
                                Latitude = reader.GetDouble(2),
                                Longitude = reader.GetDouble(3)
                            });
                        }
                    }
                }
            }

            return airports;
        }

        // to add an airport to the database
        public async Task AddAirport(Airport airport)
        {
            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO dbo.Airports(Code, City, Latitude, Longitude) VALUES(@Code, @City, @Latitude, @Longitude);";
                    cmd.Parameters.AddWithValue("@Code", airport.Code);
                    cmd.Parameters.AddWithValue("@City", airport.City);
                    cmd.Parameters.AddWithValue("@Latitude", airport.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", airport.Longitude);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // to get list of airlines
        public async Task<List<Airline>> GetAirlines()
        {
            List<Airline> airlines = new List<Airline>();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Airlines;";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            airlines.Add(new Airline()
                            {
                                Code = reader.GetString(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return airlines;
        }

        // to add an airline
        public async Task AddAirline(Airline airline)
        {
            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO dbo.Airlines(Code, Name) VALUES(@Code, @Name);";
                    cmd.Parameters.AddWithValue("@Code", airline.Code);
                    cmd.Parameters.AddWithValue("@Name", airline.Name);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // to get list of routes
        public async Task<List<Route>> GetRoutes()
        {
            List<Route> routes = new List<Route>();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Routes;";
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            routes.Add(new Route()
                            {
                                FlightNumber = reader.GetString(0),
                                Carrier = reader.GetString(1),
                                Departure = reader.GetString(2),
                                Arrival = reader.GetString(3)
                            });
                        }
                    }
                }
            }

            return routes;
        }

        // to add a route
        public async Task AddRoute(Route route)
        {
            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO dbo.Routes(FlightNumber, Carrier, Departure, Arrival) VALUES(@FlightNumber, @Carrier, @Departure, @Arrival);";
                    cmd.Parameters.AddWithValue("@FlightNumber", route.FlightNumber);
                    cmd.Parameters.AddWithValue("@Carrier", route.Carrier);
                    cmd.Parameters.AddWithValue("@Departure", route.Departure);
                    cmd.Parameters.AddWithValue("@Arrival", route.Arrival);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // to get list of flights
        public async Task<List<Flight>> GetFlights()
        {
            List<Flight> flights = new List<Flight>();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Flights;";
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flights.Add(new Flight()
                            {
                                PassengerName = reader.GetString(0),
                                PassportNumber = reader.GetString(1),
                                FlightNumber = reader.GetString(2),
                                DepartureDate = reader.GetString(3),
                                Price = reader.GetInt32(4),
                                PassengerType = reader.GetString(5)

                            });
                        }
                    }
                }
            }
            return flights;
        }

        // to get a list of routes of a specific dep airport
        public async Task<List<RouteWithSpecificAirport>> GetRoutesForSpecificAirport(string depCode)
        {
            List<RouteWithSpecificAirport> routeWithSpecifcAirports = new List<RouteWithSpecificAirport>();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT dbo.Routes.FlightNumber, dbo.Routes.Carrier, dbo.Routes.Departure, dbo.Routes.Arrival, dbo.Airports.City FROM dbo.Routes INNER JOIN dbo.Airports ON dbo.Routes.Departure = dbo.Airports.Code WHERE dbo.Routes.Departure = @DepCode ;";
                    cmd.Parameters.AddWithValue("@depCode", depCode);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            routeWithSpecifcAirports.Add(new RouteWithSpecificAirport()
                            {
                                FlightNumber = reader.GetString(0),
                                Carrier = reader.GetString(1),
                                Departure = reader.GetString(2),
                                Arrival = reader.GetString(3),
                                City = reader.GetString(4)

                            });
                        }
                    }
                }
            }
            return routeWithSpecifcAirports;
        }

        // to get the revenue of specific flight on a specific date
        public async Task<Revenue> GetRevenue(string flightNumber, string departureDate)
        {
            Revenue revenue = new Revenue();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT SUM(dbo.Flights.Price) AS Revenue, DepCity.City AS Departure, ArrCity.City As Arrival FROM dbo.Flights INNER JOIN dbo.Routes ON dbo.Flights.FlightNumber = dbo.Routes.FlightNumber INNER JOIN dbo.Airports as DepCity ON DepCity.Code = dbo.Routes.Departure INNER JOIN dbo.Airports as ArrCity ON ArrCity.Code = dbo.Routes.Arrival WHERE dbo.Flights.FlightNumber = @FlightNumber AND dbo.Routes.FlightNumber = @FlightNumber AND dbo.Flights.DepartureDate= @DepartureDate GROUP BY dbo.Flights.DepartureDate, DepCity.City, ArrCity.City;";


                    cmd.Parameters.AddWithValue("@FlightNumber", flightNumber);
                    cmd.Parameters.AddWithValue("@DepartureDate", departureDate);


                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {

                            revenue.TotalSum = reader.GetInt32(0);
                            revenue.Departure = reader.GetString(1);
                            revenue.Arrival = reader.GetString(2);

                        }
                    }
                }
            }
            return revenue;
        }

        // to add a flight
        public async Task AddFlight(Flight flight)
        {
            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO dbo.Flights(PassengerName, PassportNumber, FlightNumber, DepartureDate, Price, PassengerType) VALUES(@PassengerName, @PassportNumber, @FlightNumber, @DepartureDate, @Price, @PassengerType);";
                    cmd.Parameters.AddWithValue("@PassengerName", flight.PassengerName);
                    cmd.Parameters.AddWithValue("@PassportNumber", flight.PassportNumber);
                    cmd.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
                    cmd.Parameters.AddWithValue("@DepartureDate", flight.DepartureDate);
                    cmd.Parameters.AddWithValue("@Price", flight.Price);
                    cmd.Parameters.AddWithValue("@PassengerType", flight.PassengerType);


                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }


        // to calculate the price of the trip according to longitude and latitude of from destination and the to destination
        // then checks the type of the passenger and according to that it adds the discounts
        //0.2525 is supposed to be hardcoded baserate for all trips.
        public int PriceCalc(double fromLatitude, double fromLongitude, double toLatitude, double toLongitude, string type)
        {
            double price = 0;
            GeoCoordinate fromCord = new GeoCoordinate(fromLatitude, fromLongitude);
            GeoCoordinate toCord = new GeoCoordinate(toLatitude, toLongitude);

            double distance = fromCord.GetDistanceTo(toCord);

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
