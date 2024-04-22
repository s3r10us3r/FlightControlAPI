using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;

namespace FlightControl.DAL
{
    public class FlightRepo : BaseRepo<Flight>, IFlightRepo
    {
        public FlightRepo(FlightDbContext dbContext) : base(dbContext)
        {
        }

        public Flight? GetOne(string flightNumber)
        {
            return Table
                .Where(f => f.FlightNumber == flightNumber)
                .FirstOrDefault();
        }

        public IEnumerable<Flight> MatchByFlightNumber(string flightNumber)
        {
            return Table.Where(f => f.FlightNumber.StartsWith(flightNumber));
        }

        public IEnumerable<Flight> MatchByPlaneType(string planeType)
        {
            return Table.Where(f => f.PlaneType.StartsWith(planeType));
        }

        public IEnumerable<Flight> GetDepartureInTimeFrame(DateTime start, DateTime end)
        {
            return Table.Where(f => f.DepartureDateTime >= start && f.DepartureDateTime < end);
        }

        public IEnumerable<Flight> GetAllFromTo(int from, int to)
        {
            return Table.Where(f => f.DepartureAirportId == from  && f.ArrivalAirportId == to);
        }
    }
}
