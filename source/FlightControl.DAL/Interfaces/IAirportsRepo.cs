using FlightControl.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.DAL.Interfaces
{
    public interface IAirportsRepo : IRepo<Airport>
    {
        Airport? GetOne(string name);
        Airport? GetOneWithFlights(int id);
        Airport? GetOneWithFlights(string name);
        public IEnumerable<Airport> MatchByName(string name);
    }
}
