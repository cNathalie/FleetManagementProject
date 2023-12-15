using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;

namespace FM.Infrastructure.Mappers
{
    public class TypeWagenMapper
    {
        public static DTypeWagen MapToDTypeWagen(TypeWagen type)
        {
            var domainType = new DTypeWagen()
            {
                TypeWagenId = type.TypeWagenId,
                Type = type.Type
            };
            return domainType;
        }
    }
}
