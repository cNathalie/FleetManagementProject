using AutoMapper;
using FM_API.DTO;
using FM_Domain;

namespace FM_API;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Bestuurder, BestuurderDTO>().ReverseMap();
        CreateMap<Tankkaart, TankkaartDTO>().ReverseMap();
        CreateMap<BrandstofType, BrandstofTypeDTO>().ReverseMap();
        CreateMap<Login, LoginDTO>().ReverseMap();
    }
}
