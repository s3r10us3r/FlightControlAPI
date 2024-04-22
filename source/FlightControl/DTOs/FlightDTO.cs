using FlightControl.DAL.Models;

namespace FlightControl.DTOs
{
    public class FlightDTO
    {
        public FlightDTO()
        {
        }

        public FlightDTO(Flight flight)
        {
            Id = flight.Id;
            FlightNumber = flight.FlightNumber;
            DepartureDateTime = flight.DepartureDateTime;
            DepartureAirportId = flight.DepartureAirportId;
            ArrivalAirportId = flight.ArrivalAirportId;
            PlaneType = flight.PlaneType;
        }

        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public string PlaneType { get; set; }
    }
}
