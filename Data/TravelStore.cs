using WebApiTravel.Models.Dto;

namespace WebApiTravel.Data
{
    public static class TravelStore
    {
        public static List<TravelDTO> travelList = new List<TravelDTO>
            {
                new TravelDTO {Id = 1, Name = "Autralia" , Sqft = 100, Occupany = 4},
                new TravelDTO {Id = 2, Name = "Korean", Sqft = 300, Occupany = 3}
            };
    }
}
