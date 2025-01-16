using AutoMapper;
using carseer.Models.Domain;
using carseer.Models.DTO;

namespace carseer.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {

            CreateMap<Make, MakeDTO>()
                  .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.Make_Name))
                  .ForMember(dest => dest.MakeID, opt => opt.MapFrom(src => src.Make_ID.ToString()));
        }
    }
}
