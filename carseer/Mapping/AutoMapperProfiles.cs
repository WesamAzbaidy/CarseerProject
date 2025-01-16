using AutoMapper;
using carseer.Models.Domain;
using carseer.Models.DTO;

namespace carseer.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {

            CreateMap<Model, ModelDTO>()
            .ForMember(dest => dest.MakeID, opt => opt.MapFrom(src => src.Make_ID))
            .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.Make_Name))
            .ForMember(dest => dest.ModelID, opt => opt.MapFrom(src => src.Model_ID))
            .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model_Name));

            CreateMap<VehicleType, VehicleTypeDTO>()
            .ForMember(dest => dest.VehicleTypeName, opt => opt.MapFrom(src => src.VehicleTypeName))
            .ForMember(dest => dest.VehicleTypeId, opt => opt.MapFrom(src => src.VehicleTypeId));

            CreateMap<Make, MakeDTO>()
                  .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.Make_Name))
                  .ForMember(dest => dest.MakeID, opt => opt.MapFrom(src => src.Make_ID.ToString()));
        }

    }
}
