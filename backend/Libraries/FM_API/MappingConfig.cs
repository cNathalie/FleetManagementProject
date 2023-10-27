using AutoMapper;
using FM_Domain;

namespace FM_API;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Bestuurder, BestuurderDTO>().ReverseMap();
    }
}
