using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightControl.ServiceLayer
{
    public class FlightService : BaseService<Flight, IFlightRepo>
    {
        public FlightService(IFlightRepo repo) : base(repo)
        {
        }

        public Flight? ReadOne(string flightNumber)
        {
            return _repo.GetOne(flightNumber);
        }

        public override Flight Update(Flight entity)
        {
            if(!ValidateFlightNumber(entity.FlightNumber))
            {
                throw new InvalidOperationException("Invalid flight number!");
            }
            return base.Update(entity);
        }

        public Flight CreateOne(string flightNumber, DateTime departureDateTime, Airport departureAirport, Airport arrivalAirport, string planeType)
        {
            if (!ValidateFlightNumber(flightNumber))
            {
                throw new InvalidOperationException("Invalid flight number!");
            }
            if (_repo.GetOne(flightNumber) is not null)
            {
                throw new InvalidOperationException("Flight with this flight number already exists!");
            }

            Flight flight = new()
            {
                FlightNumber = flightNumber,
                DepartureDateTime = departureDateTime,
                DepartureAirport = departureAirport,
                ArrivalAirport = arrivalAirport,
                PlaneType = planeType
            };

            int count = _repo.Add(flight);

            if (count < 1)
            {
                throw new DbUpdateException("Failed to add record to the database!");
            }

            return flight;
        }

        public List<Flight> GetDeparturesInTimeFrame(DateTime lowerBound, DateTime higherBound)
        {
            if (higherBound < lowerBound)
            {
                throw new InvalidOperationException("Higher bound cannot be smaller than the lower bound!");
            }

            IEnumerable<Flight> flights = _repo.GetDepartureInTimeFrame(lowerBound, higherBound);
            return flights.ToList();
        }

        public List<Flight> GetAllFromTo(int from, int to)
        {
            return _repo.GetAllFromTo(from, to).ToList();
        }

        public List<Flight> GetAllFromToInTimeFrame(int from, int to, DateTime lowerBound, DateTime higherBound)
        {
            return GetAllFromTo(from, to)
                .Intersect(GetDeparturesInTimeFrame(lowerBound, higherBound))
                .ToList();
        }

        public List<Flight> MatchByFlightNumber(string flightNumber)
        {
            return _repo
                .MatchByFlightNumber(flightNumber)
                .ToList();
        }

        public List<Flight> MatchByPlaneType(string planeType)
        {
            return _repo.
                MatchByPlaneType(planeType)
                .ToList();
        }

        public List<Flight> GetAllByPlaneType(string planeType)
        {
            IEnumerable<Flight> flights = _repo.GetAll();
            return flights
                .Where(f => f.PlaneType == planeType)
                .ToList();
        }

        private bool ValidateFlightNumber(string flightNumber)
        {
            string[] parts = flightNumber.Split(" ");
            return parts.Length == 2 && ValidateAirlineCode(parts[0]) && ValidateFlightDesignator(parts[1]);
        }

        private bool ValidateAirlineCode(string airlineCode)
        {
            return airlineCode.Length == 2 && airlineCode.All(char.IsUpper);
        }

        private bool ValidateFlightDesignator(string flightDesignator)
        {
            return flightDesignator.Length <= 4 && flightDesignator.Length >= 1 && flightDesignator.All(char.IsDigit);
        }
    }
}
