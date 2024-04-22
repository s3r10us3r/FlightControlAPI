using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;

namespace FlightControl.DAL.MockRepos
{
    public class MockAirports : MockRepo<Airport>, IAirportsRepo
    {
        public MockAirports() : base()
        {
        }

        protected override List<Airport> Initiate()
        {
            return [
                new() {Id = 1, Name = "To delete1"},
                new() {Id = 2, Name = "To delete2"},
                new() {Id = 3, Name = "Warsaw Airport"},
                new() {Id = 4, Name = "Paris Airport"},
                new() {Id = 5, Name = "Berlin Airport"},
                new() {Id = 6, Name = "CPK Airport"},
                ];
        }

        public Airport? GetOne(string name)
        {
            return _db.Find(a => a.Name == name);
        }

        public Airport? GetOneWithFlights(int id)
        {
            Airport? airport = GetOne(id);
            if (airport is null)
            {
                return airport;
            }
            airport.DepartureFlights =
                [
                    new() {Id = 1, ArrivalAirportId = 0, DepartureAirportId = id, FlightNumber = "TT 1", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                    new() {Id = 2, ArrivalAirportId = 0, DepartureAirportId = id, FlightNumber = "TT 2", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                    new() {Id = 3, ArrivalAirportId = 0, DepartureAirportId = id, FlightNumber = "TT 3", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                ];
            airport.ArrivalFlights =
                [
                    new() {Id = 4, ArrivalAirportId = id, DepartureAirportId = 0, FlightNumber = "TT 4", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                    new() {Id = 5, ArrivalAirportId = id, DepartureAirportId = 0, FlightNumber = "TT 5", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                    new() {Id = 6, ArrivalAirportId = id, DepartureAirportId = 0, FlightNumber = "TT 6", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                ];
            return airport;
        }

        public Airport? GetOneWithFlights(string name)
        {
            Airport? airport = GetOne(name);
            if (airport is null)
            {
                return airport;
            }
            int id = airport.Id;
            airport.DepartureFlights =
                [
                    new() {Id = 1, ArrivalAirportId = 0, DepartureAirportId = id, FlightNumber = "TT 1", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                    new() {Id = 2, ArrivalAirportId = 0, DepartureAirportId = id, FlightNumber = "TT 2", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                    new() {Id = 3, ArrivalAirportId = 0, DepartureAirportId = id, FlightNumber = "TT 3", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                ];
            airport.ArrivalFlights =
                [
                    new() {Id = 4, ArrivalAirportId = id, DepartureAirportId = 0, FlightNumber = "TT 4", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                    new() {Id = 5, ArrivalAirportId = id, DepartureAirportId = 0, FlightNumber = "TT 5", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                    new() {Id = 6, ArrivalAirportId = id, DepartureAirportId = 0, FlightNumber = "TT 6", DepartureDateTime = DateTime.UtcNow, PlaneType="Test"},
                ];
            return airport;
        }

        public IEnumerable<Airport> MatchByName(string name)
        {
            return _db.Where(a => a.Name.StartsWith(name));
        }
    }
}
