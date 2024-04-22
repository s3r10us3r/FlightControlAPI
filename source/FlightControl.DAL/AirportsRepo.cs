using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightControl.DAL
{
    public class AirportsRepo : BaseRepo<Airport>, IAirportsRepo
    {
        public AirportsRepo(FlightDbContext dbContext) : base(dbContext)
        {
        }

        public Airport? GetOne(string name)
        {
            return Table
                .Where(a => a.Name == name)
                .FirstOrDefault();
        }

        public Airport? GetOneWithFlights(int id)
        {
            Airport? airport = Table.
                Include(a => a.DepartureFlights)
               .Include(a => a.ArrivalFlights)
               .FirstOrDefault(a => a.Id == id);

            return airport;
        }

        public Airport? GetOneWithFlights(string name)
        {
            Airport? airport = Table.
                Include(a => a.DepartureFlights)
               .Include(a => a.ArrivalFlights)
               .FirstOrDefault(a => a.Name == name);

            return airport;
        }

        public IEnumerable<Airport> MatchByName(string name)
        {
            return Table.Where(x => x.Name.StartsWith(name));
        }
    }
}
