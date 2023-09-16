using System.ComponentModel.DataAnnotations;

namespace WebApiTravel.Models.Dto
{
    public class TravelDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupany { get; set; }
        public int Sqft { get; set; }
    }
}
