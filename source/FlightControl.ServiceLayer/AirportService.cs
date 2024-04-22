using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;


namespace FlightControl.ServiceLayer
{
    public class AirportService : BaseService<Airport, IAirportsRepo>
    {
        public AirportService(IAirportsRepo repo) : base(repo)
        {
        }

        public Airport CreateOne(string name)
        {
            if (DoesNameExist(name))
            {
                throw new InvalidOperationException("Airport with this name already exists!");
            }
            Airport airport = new Airport()
            {
                Name = name
            };
            _repo.Add(airport);
            return airport;
        }

        public Airport? ReadOne(string name)
        {
            return _repo.GetOne(name);
        }

        public Airport? ReadOneWithFlights(string name)
        {
            return _repo.GetOneWithFlights(name);
        }

        public Airport? ReadOneWithFlights(int id)
        {
            return _repo.GetOneWithFlights(id);
        }

        public Airport ChangeName(string name, int id)
        {
            if (DoesNameExist(name))
            {
                throw new InvalidOperationException("Airport with this name already exists!");
            }
            Airport? airport = _repo.GetOne(id);
            if (airport is null)
            {
                throw new InvalidOperationException("Airport with this id does not exist!");
            }
            airport.Name = name;
            int changed = _repo.Save(airport);
            return airport;
        }

        public List<Airport> MatchByName(string name)
        {
            return _repo.MatchByName(name).ToList();
        }

        public new int DeleteOne(int id)
        {
            Airport? airport = _repo.GetOneWithFlights(id);
            if (airport is null)
            {
                return 0;
            }
            if ((airport.ArrivalFlights is not null && airport.ArrivalFlights.Count > 0) || (airport.DepartureFlights is not null && airport.DepartureFlights.Count > 0))
            {
                Console.WriteLine(airport.ArrivalFlights.Count);
                Console.WriteLine(airport.DepartureFlights.Count);
                throw new InvalidOperationException("To be deleted airport must not have any flights connected to it.");
            }
            return _repo.Remove(airport);
        }

        private bool DoesNameExist(string name)
        {
            var match = _repo.GetOne(name);
            return match is not null;
        }
    }
}
