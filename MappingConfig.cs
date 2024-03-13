using AutoMapper;
using WebApiTravel.Models;
using WebApiTravel.Models.Dto;

namespace WebApiTravel
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Travel, TravelDTO>();
            CreateMap<TravelDTO, Travel>();
            CreateMap<Travel, TravelCreateDTO>().ReverseMap();
            CreateMap<Travel, TravelUpdateDTO>().ReverseMap();

            CreateMap<TravelNumber, TravelNumberDTO>().ReverseMap();
            CreateMap<TravelNumber, TravelNumberCreateDTO>().ReverseMap();
            CreateMap<TravelNumber, TravelNumberUpdateDTO>().ReverseMap();
            
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
