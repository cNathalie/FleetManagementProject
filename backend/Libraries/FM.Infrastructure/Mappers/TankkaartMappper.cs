using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;

namespace FM.Infrastructure.Mappers
{
    public class TankkaartMappper
    {
        public static DTankkaart MapToDTankkaart(Tankkaart tankaart)
        {
            var domainTankkaart = new DTankkaart()
            {
                TankkaartId = tankaart.TankkaartId,
                Kaartnummer = tankaart.Kaartnummer,
                Geldigheidsdatum = tankaart.Geldigheidsdatum,
                Pincode = tankaart.Pincode,
                Brandstoftype = tankaart.BrandstofType.Type,
                IsActief = tankaart.Actief,
            };
            return domainTankkaart;
        }

        public static Tankkaart MapToEfTankkaart(DTankkaart tankaart, BrandstofType brandstof)
        {
            var efTankkaart = new Tankkaart()
            {
                Kaartnummer = tankaart.Kaartnummer,
                Geldigheidsdatum = tankaart.Geldigheidsdatum,
                Pincode = tankaart.Pincode,
                BrandstofTypeId = brandstof.BrandstofTypeId,
                Actief = tankaart.IsActief
            };
            return efTankkaart;
        }
    }
}
