using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlightControl.DAL.Models
{

    [Index(nameof(FlightNumber), IsUnique = true)]
    public class Flight : EntityBase
    {
        [Required]
        [StringLength(7, MinimumLength = 4)]
        public string FlightNumber { get; set; }
        [Required]
        public DateTime DepartureDateTime { get; set; }
        [Required]
        public int DepartureAirportId { get; set; }
        [Required]
        public int ArrivalAirportId { get; set; }
        [Required]
        [StringLength(50)]
        public string PlaneType { get; set; }

        public virtual Airport DepartureAirport { get; set; }
        public virtual Airport ArrivalAirport { get; set; }
    }
}
