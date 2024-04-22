using FlightControl.Data.Models;

namespace FlightControlTests.Data
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
                    FlightNumber = "LOT 00000",
                    DepartureDateTime = new DateTime(2024, 5, 6, 23, 34, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[2],
                    PlaneType = "Boeing"
                },
                new() {
                    FlightNumber = "RNA 12345",
                    DepartureDateTime = new DateTime(2010, 3, 5, 11, 10, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[5],
                    PlaneType = "Airbus"
                },
                new() {
                    FlightNumber = "EMT 21370",
                    DepartureDateTime = new DateTime(2019, 12, 6, 10, 13, 0),
                    DepartureAirport = airports[2],
                    ArrivalAirport = airports[5],
                    PlaneType = "Dreamliner"
                },
                new() {
                    FlightNumber = "WZA 20157",
                    DepartureDateTime = new DateTime(2014, 8, 10, 3, 15, 0),
                    DepartureAirport = airports[4],
                    ArrivalAirport = airports[3],
                    PlaneType = "Comac"
                },
                new() {
                    FlightNumber = "SPN 17634",
                    DepartureDateTime = new DateTime(2024, 10, 7, 23, 34, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[2],
                    PlaneType = "Boeing"
                },
                new() {
                    FlightNumber = "BLT 14823",
                    DepartureDateTime = new DateTime(2003, 6, 4, 6, 15, 0),
                    DepartureAirport = airports[5],
                    ArrivalAirport = airports[2],
                    PlaneType = "Boeing"
                },
                new() {
                    FlightNumber = "LOT 19980",
                    DepartureDateTime = new DateTime(2024, 5, 6, 23, 34, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[2],
                    PlaneType = "Comac"
                },
                new() {
                    FlightNumber = "LOT 21340",
                    DepartureDateTime = new DateTime(2004, 10, 7, 20, 30, 0),
                    DepartureAirport = airports[3],
                    ArrivalAirport = airports[4],
                    PlaneType = "Airbus"
                },
                new() {
                    FlightNumber = "BLT 22222",
                    DepartureDateTime = new DateTime(2024, 5, 6, 22, 10, 0),
                    DepartureAirport = airports[2],
                    ArrivalAirport = airports[3],
                    PlaneType = "Boeing"
                }
            };
            flights.ForEach(x => context.Flights.Add(x));

            var users = new List<User>()
            {
                new() {Login = "TestAdmin1", PasswordHash="NotHashedtestAdmin2", IsAdmin = true},
                new() {Login = "TestUser1", PasswordHash="TestUser1", IsAdmin = false}
            };
            users.ForEach(x => context.Users.Add(x));

            var jwtokens = new List<JwToken>()
            {
                new() {Token = "dhuah9uds89ey21h9qd", IsRevoked = false, User = users[1]},
                new() {Token = "d8y9qeu891892e1hdadwad", IsRevoked = true, User = users[1]},
                new() {Token = "thisisadmintokesdioajidas", IsRevoked = false, User = users[0]},
                new() {Token = "revokedamsdndjsminasdsadtmdsoken", IsRevoked = true, User = users[0]}
            };
            jwtokens.ForEach(x => context.JwTokens.Add(x));

            context.SaveChanges();
        }
    }
}
