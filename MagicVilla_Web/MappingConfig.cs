using AutoMapper;
using MagicVilla_Web.Models.DTOS;

namespace MagicVilla_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            
            CreateMap<VillaDto,VillaCreacionDto>().ReverseMap();
            CreateMap<VillaDto, VillaActualizacionDto>().ReverseMap();

            CreateMap<NumeroVillaDTO, NumeroVillaCreateDTO>().ReverseMap();
            CreateMap<NumeroVillaDTO, NumeroVillaUpdateDTO>().ReverseMap();
        }
    }
}
