using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;
using FM.Infrastructure.Exceptions;

namespace FM.Infrastructure.Mappers
{
    public class BestuurderMapper
    {
        public static DBestuurder MapToDBestuurder(Bestuurder entity, List<TypeRijbewijs> types)
        {
            var rijbewijs = types.Where(tr => tr.TypeRijbewijsId == entity.TyperijbewijsId).FirstOrDefault() ?? throw new EFBestuurderRepoException("Could not map to domain model: 'RijbewijsType' is null");
            var dBestuurder = new DBestuurder()
            {
                BestuurderId = entity.BestuurderId,
                Naam = entity.Naam,
                Voornaam = entity.Voornaam,
                Adres = entity.Adres,
                Rijksregisternummer = entity.Rijksregisternummer,
                Rijbewijs = rijbewijs!.Type
            };
            return dBestuurder;
        }

        public static DBestuurder MapToDBestuurder(Bestuurder entity, TypeRijbewijs type)
        {
            var rijbewijs = type.Type ?? throw new EFBestuurderRepoException("Could not map to domain model: 'RijbewijsType' is null");
            var dBestuurder = new DBestuurder()
            {
                BestuurderId = entity.BestuurderId,
                Naam = entity.Naam,
                Voornaam = entity.Voornaam,
                Adres = entity.Adres,
                Rijksregisternummer = entity.Rijksregisternummer,
                Rijbewijs = rijbewijs!
            };
            return dBestuurder;
        }

        public static Bestuurder MapToEFBestuurder(DBestuurder bestuurder, TypeRijbewijs efRijbewijs)
        {
            var newEntity = new Bestuurder()
            {
                Naam = bestuurder.Naam!,
                Voornaam = bestuurder.Voornaam!,
                Adres = bestuurder.Adres!,
                Rijksregisternummer = bestuurder.Rijksregisternummer!,
                TyperijbewijsId = efRijbewijs.TypeRijbewijsId
            };
            return newEntity;
        }
    }
}
