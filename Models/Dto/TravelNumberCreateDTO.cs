using System.ComponentModel.DataAnnotations;

namespace WebApiTravel.Models.Dto
{
    public class TravelNumberCreateDTO
    {
        [Required]

        public int TravelNo { get; set; }
        [Required]
        public int TravelId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
