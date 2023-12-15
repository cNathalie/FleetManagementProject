using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;

namespace FM.Infrastructure.Mappers
{
    public class FleetMemberMapper
    {
        public static DFleetMemberVerbose MapToDFleetMember(FleetMember entity)
        {
            var domainMember = new DFleetMemberVerbose()
            {
                FleetId = entity.FleetId,
                BestuurderNaam = entity.Bestuurder.Naam,
                BestuurderVoornaam = entity.Bestuurder.Voornaam,
                TankaartId = entity.TankkaartId,
                VoertuigMerkModel = entity.Voertuig.MerkEnModel,
                VoertuigNummerplaat = entity.Voertuig.Nummerplaat,
                VoertuigChassisnummer = entity.Voertuig.Chassisnummer,
            };
            return domainMember;
        }

        public static FleetMember MapToEfFleetMember(DFleetMemberIds member)
        {
            var efMember = new FleetMember()
            {
                FleetId = member.FleetId,
                BestuurderId = member.BestuurderId,
                TankkaartId = member.TankkaartId,
                VoertuigId = member.VoertuigId,
            };
            return efMember;
        }

        public static DFleetMemberVerbose MapToDFleetMemberVerbose(int id, Bestuurder b, Tankkaart t, Voertuig v)
        {
            var verboseMember = new DFleetMemberVerbose()
            {
                FleetId = id,
                BestuurderNaam = b.Naam,
                BestuurderVoornaam = b.Voornaam,
                TankaartId = t.TankkaartId,
                VoertuigChassisnummer = v.Chassisnummer,
                VoertuigMerkModel = v.MerkEnModel,
                VoertuigNummerplaat = v.Nummerplaat
            };
            return verboseMember;
        }
    }
}
