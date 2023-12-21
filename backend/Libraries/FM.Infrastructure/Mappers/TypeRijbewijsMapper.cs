using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;

namespace FM.Infrastructure.Mappers
{
    public class RijbewijsMapper
    {
        public static DTypeRijbewijs MapToDTypeRijbewijs(TypeRijbewijs rijbewijs)
        {
            var domainRijbewijs = new DTypeRijbewijs()
            {
                TypeRijbewijsId = rijbewijs.TypeRijbewijsId,
                Type = rijbewijs.Type
            };
            return domainRijbewijs;
        }
    }
}
