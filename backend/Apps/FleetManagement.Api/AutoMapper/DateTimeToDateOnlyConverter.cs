using AutoMapper;

namespace FleetManagement.Api.AutoMapper
{
    public partial class MappingConfig
    {
        public class DateTimeToDateOnlyConverter : ITypeConverter<DateTime, DateOnly>
        {
            public DateOnly Convert(DateTime source, DateOnly destination, ResolutionContext context)
            {
                return new DateOnly(source.Year, source.Month, source.Day);
            }
        }
    }
}
