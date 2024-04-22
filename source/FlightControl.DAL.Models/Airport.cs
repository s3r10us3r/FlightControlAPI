using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightControl.DAL.Models
{
    public class Airport : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty(nameof(Flight.DepartureAirport))]
        public virtual ICollection<Flight> DepartureFlights { get; set; } = new HashSet<Flight>();

        [InverseProperty(nameof(Flight.ArrivalAirport))]
        public virtual ICollection<Flight> ArrivalFlights { get; set; } = new HashSet<Flight>();
    }
}
