using AutoMapper;
using Magic_Villa_API.Modelos;
using Magic_Villa_API.Modelos.DTOS;

namespace Magic_Villa_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa,VillaDto>().ReverseMap();
            CreateMap<Villa,VillaCreacionDto>().ReverseMap();
            CreateMap<Villa,VillaActualizacionDto>().ReverseMap();


            CreateMap<NumeroVilla,NumeroVillaDTO>().ReverseMap(); ;
            CreateMap<NumeroVilla, NumeroVillaCreateDTO>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaUpdateDTO>().ReverseMap();
        }
    }
}
