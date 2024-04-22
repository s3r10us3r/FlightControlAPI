using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightControl.DAL.Models
{
    public class User : EntityBase
    {
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Login { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }
    }
}
