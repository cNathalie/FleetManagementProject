using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;

namespace FM.Infrastructure.Mappers
{
    public class VoertuigMapper
    {
        public static DVoertuig MapToDVoertuig(Voertuig voertuig)
        {
            var domainVoertuig = new DVoertuig()
            {
                VoertuigId = voertuig.VoertuigId,
                MerkEnModel = voertuig.MerkEnModel,
                Chassisnummer = voertuig.Chassisnummer,
                Nummerplaat = voertuig.Nummerplaat,
                BrandstofType = voertuig.BrandstofType.Type,
                TypeWagen = voertuig.TypeWagen.Type,
                Kleur = voertuig.Kleur,
                AantalDeuren = voertuig.AantalDeuren
            };
            return domainVoertuig;
        }

        public static Voertuig MapToEfVoertuig(DVoertuig voertuig, BrandstofType brandstof, TypeWagen typeWagen)
        {
            var efVoertuig = new Voertuig()
            {
                MerkEnModel = voertuig.MerkEnModel!,
                Chassisnummer = voertuig.Chassisnummer!,
                Nummerplaat = voertuig.Nummerplaat!,
                BrandstofType = brandstof,
                TypeWagen = typeWagen,
                Kleur = voertuig.Kleur!,
                AantalDeuren = voertuig.AantalDeuren
            };
            return efVoertuig;
        }
    }
}
