using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;

namespace FlightControl.DAL.MockRepos
{
    public class MockFlight : MockRepo<Flight>, IFlightRepo
    {
        public MockFlight() : base()
        {
        }

        public IEnumerable<Flight> GetAllFromTo(int from, int to)
        {
            return _db.Where(f => f.ArrivalAirportId == to && f.DepartureAirportId == from);
        }

        public IEnumerable<Flight> GetDepartureInTimeFrame(DateTime start, DateTime end)
        {
            return _db.Where(f => f.DepartureDateTime >= start && f.DepartureDateTime <= end);
        }

        public Flight? GetOne(string flightNumber)
        {
            return _db.Find(f => f.FlightNumber == flightNumber);
        }

        public IEnumerable<Flight> MatchByFlightNumber(string flightNumber)
        {
            return _db.Where(f => f.FlightNumber.StartsWith(flightNumber));
        }

        public IEnumerable<Flight> MatchByPlaneType(string planeType)
        {
            return _db.Where(f => f.PlaneType.StartsWith(planeType));
        }

        protected override List<Flight> Initiate()
        {
            return [
                new() {
                    Id = 1, 
                    FlightNumber = "LT 0000",
                    DepartureDateTime = new DateTime(2024, 5, 6, 23, 34, 0),
                    DepartureAirportId = 3,
                    ArrivalAirportId = 2,
                    PlaneType = "Boeing"
                },
                new() {
                    Id = 2,
                    FlightNumber = "RN 1234",
                    DepartureDateTime = new DateTime(2010, 3, 5, 11, 10, 0),
                    DepartureAirportId = 3,
                    ArrivalAirportId = 5,
                    PlaneType = "Airbus"
                },
                new() {
                    Id = 3,
                    FlightNumber = "EM 2137",
                    DepartureDateTime = new DateTime(2019, 12, 6, 10, 13, 0),
                    DepartureAirportId = 2,
                    ArrivalAirportId = 5,
                    PlaneType = "Dreamliner"
                },
                new() {
                    Id = 4,
                    FlightNumber = "WZ 2015",
                    DepartureDateTime = new DateTime(2014, 8, 10, 3, 15, 0),
                    DepartureAirportId = 4,
                    ArrivalAirportId = 3,
                    PlaneType = "Comac"
                },
                new() {
                    Id = 5,
                    FlightNumber = "SP 1763",
                    DepartureDateTime = new DateTime(2024, 10, 7, 23, 34, 0),
                    DepartureAirportId = 3,
                    ArrivalAirportId = 2,
                    PlaneType = "Boeing"
                },
                new() {
                    Id = 6,
                    FlightNumber = "BL 1423",
                    DepartureDateTime = new DateTime(2003, 6, 4, 6, 15, 0),
                    DepartureAirportId = 5,
                    ArrivalAirportId = 2,
                    PlaneType = "Boeing"
                },
                new() {
                    Id = 7,
                    FlightNumber = "LT 1998",
                    DepartureDateTime = new DateTime(2020, 5, 6, 23, 34, 0),
                    DepartureAirportId = 3,
                    ArrivalAirportId = 2,
                    PlaneType = "Comac"
                },
                new() {
                    Id = 8,
                    FlightNumber = "LT 2134",
                    DepartureDateTime = new DateTime(2004, 10, 7, 20, 30, 0),
                    DepartureAirportId = 3,
                    ArrivalAirportId = 4,
                    PlaneType = "Airbus"
                },
                new() {
                    Id = 9,
                    FlightNumber = "BT 2222",
                    DepartureDateTime = new DateTime(2024, 5, 6, 22, 10, 0),
                    DepartureAirportId = 2,
                    ArrivalAirportId = 3,
                    PlaneType = "Boeing"
                }
                ];
        }
    }
}
