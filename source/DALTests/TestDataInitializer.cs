using FlightControl.DAL;
using FlightControl.DAL.Models;

namespace DALTests
{
    internal class TestDataInitializer
    {
        public static void RecreateDatabase(FlightDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Seed(context);
        }

        private static void Seed(FlightDbContext context)
        {
            var airports = new List<Airport>
            {
                new() {Name = "To delete1"},
                new() {Name = "To delete2"},
                new() {Name = "Warsaw Airport"},
                new() {Name = "Paris Airport"},
                new() {Name = "Berlin Airport"},
                new() {Name = "CPK Airport"},
            };
            airports.ForEach(x => context.Airports.Add(x));

            var flights = new List<Flight>
            {
                new() {
                    FlightNumber = "LT 0000",
                    DepartureDateTime = new DateTime(2024, 5, 6, 23, 34, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[2],
                    PlaneType = "Boeing"
                },
                new() {
                    FlightNumber = "RN 1234",
                    DepartureDateTime = new DateTime(2010, 3, 5, 11, 10, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[5],
                    PlaneType = "Airbus"
                },
                new() {
                    FlightNumber = "EM 2137",
                    DepartureDateTime = new DateTime(2019, 12, 6, 10, 13, 0),
                    DepartureAirport = airports[2],
                    ArrivalAirport = airports[5],
                    PlaneType = "Dreamliner"
                },
                new() {
                    FlightNumber = "WZ 2015",
                    DepartureDateTime = new DateTime(2014, 8, 10, 3, 15, 0),
                    DepartureAirport = airports[4],
                    ArrivalAirport = airports[3],
                    PlaneType = "Comac"
                },
                new() {
                    FlightNumber = "SP 1763",
                    DepartureDateTime = new DateTime(2024, 10, 7, 23, 34, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[2],
                    PlaneType = "Boeing"
                },
                new() {
                    FlightNumber = "BL 1423",
                    DepartureDateTime = new DateTime(2003, 6, 4, 6, 15, 0),
                    DepartureAirport = airports[5],
                    ArrivalAirport = airports[2],
                    PlaneType = "Boeing"
                },
                new() {
                    FlightNumber = "LT 1998",
                    DepartureDateTime = new DateTime(2024, 5, 6, 23, 34, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[2],
                    PlaneType = "Comac"
                },
                new() {
                    FlightNumber = "LT 2134",
                    DepartureDateTime = new DateTime(2004, 10, 7, 20, 30, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[4],
                    PlaneType = "Airbus"
                },
                new() {
                    FlightNumber = "BT 2222",
                    DepartureDateTime = new DateTime(2024, 5, 6, 22, 10, 0),
                    DepartureAirport = airports[2],
                    ArrivalAirport = airports[3],
                    PlaneType = "Boeing"
                }
            };
            flights.ForEach(x => context.Flights.Add(x));

            var users = new List<User>()
            {
                new() {Login = "TestUser2", PasswordHash="NotHashedtestUser2"},
                new() {Login = "UserToDelete", PasswordHash="ToDelete"},
                new() {Login = "TestUser1", PasswordHash="TestUser1"}
            };
            
            users.ForEach(x => context.Users.Add(x));
            context.SaveChanges();
        }
    }
}
