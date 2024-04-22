using FlightControl.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.DAL.Interfaces
{
    public interface IFlightRepo : IRepo<Flight>
    {
        Flight? GetOne(string flightNumber);
        IEnumerable<Flight> MatchByFlightNumber(string flightNumber);
        IEnumerable<Flight> MatchByPlaneType(string planeType);
        IEnumerable<Flight> GetDepartureInTimeFrame(DateTime start, DateTime end);
        IEnumerable<Flight> GetAllFromTo(int from, int to);
    }
}
