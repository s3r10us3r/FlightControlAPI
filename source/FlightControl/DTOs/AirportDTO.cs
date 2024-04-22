using FlightControl.DAL.Models;

namespace FlightControl.DTOs
{
    public class AirportDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AirportDTO(Airport airport)
        {
            Id = airport.Id;
            Name = airport.Name;
        }
    }
}
