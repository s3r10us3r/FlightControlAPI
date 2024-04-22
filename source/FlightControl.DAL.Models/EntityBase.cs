using System.ComponentModel.DataAnnotations;

namespace FlightControl.DAL.Models
{
    //This is here only to enable generics in BaseRepo
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
