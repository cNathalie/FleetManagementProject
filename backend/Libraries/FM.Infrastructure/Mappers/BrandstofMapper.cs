using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;

namespace FM.Infrastructure.Mappers
{
    public class BrandstofMapper
    {
        public static DBrandstofType MapToDBrandstof(BrandstofType brandstof)
        {
            var domainBrandstof = new DBrandstofType()
            {
                BrandstofTypeId = brandstof.BrandstofTypeId,
                Type = brandstof.Type,
            };
            return domainBrandstof;
        }

    }
}
