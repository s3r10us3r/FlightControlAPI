using FlightControl.DAL.Models;

namespace FlightControl.DTOs
{
    public class AirportDTOFlights
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FlightDTO> DepartureFlights { get; set; }
        public IEnumerable<FlightDTO> ArrivalFlights { get; set; }

        public AirportDTOFlights(Airport airport)
        {
            Id = airport.Id;
            Name = airport.Name;
            DepartureFlights = airport.DepartureFlights.Select(f => new FlightDTO(f));
            ArrivalFlights = airport.ArrivalFlights.Select(f => new FlightDTO(f));
        }
    }
}
