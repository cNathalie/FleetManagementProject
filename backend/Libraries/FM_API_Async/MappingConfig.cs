using AutoMapper;
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
        CreateMap<Login, AuthLoginDTO>().ReverseMap();  
        CreateMap<FleetMember, FleetMemberDTO>().ReverseMap();
        CreateMap<TypeRijbewijs, TypeRijbewijsDTO>().ReverseMap();
        CreateMap<TypeWagen, TypeWagenDTO>().ReverseMap();
        CreateMap<Voertuig, VoertuigDTO>().ReverseMap();
    }
}
