using AutoMapper;
using FleetManagement.Api.DTOs.Bestuurder;
using FleetManagement.Api.DTOs.BrandstofType;
using FleetManagement.Api.DTOs.FleetMember;
using FleetManagement.Api.DTOs.Tankkaart;
using FleetManagement.Api.DTOs.TypeRijbewijs;
using FleetManagement.Api.DTOs.TypeWagen;
using FleetManagement.Api.DTOs.User;
using FleetManagement.Api.DTOs.Voertuig;
using FM.Domain.Models;
using FM.Infrastructure.Resources;

namespace FleetManagement.Api.AutoMapper
{
    public partial class MappingConfig : Profile
    {
        public MappingConfig() 
        { 
            CreateMap<DBestuurder, BestuurderOutgoingDTO>().ReverseMap();
            CreateMap<DBestuurder, BestuurderIncomingDTO>().ReverseMap();
            CreateMap<DBestuurder, BestuurderIncomingUpdateDTO>().ReverseMap();

            CreateMap<DBrandstofType, BrandstofTypeOutgoingDTO>().ReverseMap();

            CreateMap<DFleetMemberVerbose, FleetMemberOutgoingDTO>().ReverseMap();
            CreateMap<DFleetMemberIds, FleetMemberUpdateIdsIncomingDTO>().ReverseMap();
            CreateMap<DFleetMemberIds, FleetMemberIdsNewIncompingDTO>().ReverseMap();

            CreateMap<DateTime, DateOnly>().ConvertUsing<DateTimeToDateOnlyConverter>(); 
            CreateMap<DateOnly, DateTime>().ConvertUsing<DateOnlyToDateTimeConverter>();
            CreateMap<DTankkaart, TankkaartOutgoingDTO>().ReverseMap();
            CreateMap<DTankkaart, TankkaartNewIncomingDTO>().ReverseMap();
            CreateMap<DTankkaart, TankkaartUpdateIncomingDTO>().ReverseMap();

            CreateMap<DTypeRijbewijs, TypeRijbewijsOutgoingDTO>().ReverseMap();

            CreateMap<DVoertuig, VoertuigOutgoingDTO>().ReverseMap();
            CreateMap<DVoertuig, VoertuigIncomingDTO>().ReverseMap();
            CreateMap<DVoertuig, VoertuigUpdateIncomingDTO>().ReverseMap();

            CreateMap<DTypeWagen, TypeWagenOutgoingDTO>().ReverseMap();

            CreateMap<LoginResource, LoginUserDTO>().ReverseMap();
            CreateMap<RegisterResource, RegisterUserDTO>().ReverseMap();
            CreateMap<UserResource, UserOutgoingDTO>().ReverseMap();
            CreateMap<AuthenticatedUserResource, AuthenticatedUserDTO>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Tokens!.AccessToken!))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Tokens!.RefreshToken))
                .ReverseMap();
            CreateMap<DUser, UserOutgoingDTO>().ReverseMap();
            CreateMap<Tokens, TokensDTO>().ReverseMap();

        }
    }
}
